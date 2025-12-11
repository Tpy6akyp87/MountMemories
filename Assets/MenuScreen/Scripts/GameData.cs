using Newtonsoft.Json;  // ← НОВОЕ: для Json.NET
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = System.Random;

// ======================= ДАННЫЕ =======================
[System.Serializable]  // Оставляем для Unity Inspector, но не обязательно
public class GameData
{
    //public PlayerData[] playerDatas = new PlayerData[0];
    public PlayerData playerDatas;
    // Здесь можешь добавить любые другие поля: уровень, золото, настройки и т.д.
}

[System.Serializable]
public class PlayerData
{
    public string playername;
    public string warName;
    public string runeName;
    public string rogName;
    public int warHP;
    public int runeHP;
    public int rogHP;
    public int warDamage;
    public int runeDamage;
    public int rogDamage;

    // Пустой конструктор (не обязателен, но на всякий случай)
    public PlayerData() { }
}

// ======================= СИСТЕМА СОХРАНЕНИЙ =======================
public static class SaveSystem
{
    private static readonly string SaveFolder = Path.Combine(Application.persistentDataPath, "Saves");
    private const string Extension = ".json"; // теперь сохраняем в читаемом JSON

    private static string GetSavePath(string saveName)
    {
        return Path.Combine(SaveFolder, saveName + Extension);
    }

    private static void EnsureFolderExists()
    {
        if (!Directory.Exists(SaveFolder))
            Directory.CreateDirectory(SaveFolder);
    }

    // ================================================
    // 1. Создание нового профиля
    // ================================================
    public static void CreateNewSave(string saveName)
    {
        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("Имя профиля пустое!");
            return;
        }

        EnsureFolderExists();
        List<string> warNames = new List<string> { "war1", "war2", "war33", "war4" };
        List<string> runeNames = new List<string> { "rune1", "rune2", "rune3", "rune4" };
        List<string> rogNames = new List<string> { "rog1", "rog2", "rog3", "rog4" };
        Random rnd = new Random();
        int randomIndex = rnd.Next(warNames.Count);
        GameData newData = new GameData();
        newData.playerDatas = new PlayerData();
        newData.playerDatas.playername = saveName;
        newData.playerDatas.warName = warNames[randomIndex];
        newData.playerDatas.runeName = runeNames[randomIndex];
        newData.playerDatas.rogName = rogNames[randomIndex];
        // При желании можно сразу добавить первого игрока:
        // newData.playerDatas = new PlayerData[1] { new PlayerData { playername = saveName } };  // Исправил: используй массив, а не Add()

        Save(saveName, newData);
        Debug.Log($"Создан новый профиль: {saveName}");
    }

    // ================================================
    // 2. Сохранение текущих данных
    // ================================================
    public static void Save(string saveName, GameData data)
    {
        EnsureFolderExists();

        // ← ИЗМЕНЕНО: Используем Json.NET
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);  // Indented = красивый отступ
        string path = GetSavePath(saveName);

        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"Сохранено в: {path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка записи файла сохранения: {e.Message}");
        }
    }

    // ================================================
    // 3. Загрузка (с кэшированием в одной сессии!)
    // ================================================
    private static readonly Dictionary<string, GameData> loadedSaves = new Dictionary<string, GameData>();

    public static GameData Load(string saveName)
    {
        if (loadedSaves.TryGetValue(saveName, out GameData cached))
            return cached;

        string path = GetSavePath(saveName);
        if (!File.Exists(path)) return null;

        string json = File.ReadAllText(path);
        // ← ИЗМЕНЕНО: Используем Json.NET
        GameData data = JsonConvert.DeserializeObject<GameData>(json);

        // Важно: если в файле был пустой массив — сделаем его изменяемым
        if (data?.playerDatas == null)
            data.playerDatas = new PlayerData();

        loadedSaves[saveName] = data;
        return data;
    }

    // ================================================
    // 4. Удаление профиля
    // ================================================
    public static bool DeleteSave(string saveName)
    {
        string path = GetSavePath(saveName);

        if (File.Exists(path))
        {
            File.Delete(path);
            loadedSaves.Remove(saveName);
            Debug.Log($"Профиль удалён: {saveName}");
            return true;
        }

        Debug.LogWarning($"Попытка удалить несуществующий профиль: {saveName}");
        return false;
    }

    // ================================================
    // 5. Получить список всех профилей
    // ================================================
    public static List<string> GetAllSaveNames()
    {
        EnsureFolderExists();

        string[] files = Directory.GetFiles(SaveFolder, "*" + Extension);
        List<string> names = new List<string>();

        foreach (string file in files)
        {
            names.Add(Path.GetFileNameWithoutExtension(file));
        }

        return names;
    }

    // ================================================
    // 6. Очистка кэша (вызывай при выходе из игры или смене аккаунта)
    // ================================================
    public static void ClearCache()
    {
        loadedSaves.Clear();
        Debug.Log("Кэш сохранений очищен");
    }
}