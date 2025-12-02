using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // если используешь TextMeshPro, иначе замени на UnityEngine.UI.Text

public class MainMenu : MonoBehaviour
{
    [Header("UI Элементы")]
    public TMP_InputField inputFieldNewProfile;     // Поле ввода имени нового профиля
    public Button buttonCreateNew;                  // Кнопка "Создать"
    public Transform savesListParent;               // Родительский объект (например, Content у ScrollView)
    public GameObject saveSlotPrefab;               // Префаб одного слота профиля (с Text + 2 кнопки)

    [Header("Другие настройки")]
    public Button buttonPlay;                       // Кнопка "Играть" (активируется после выбора профиля)
    public TextMeshProUGUI currentProfileText;      // Показывает текущий выбранный профиль

    // Внутренние данные
    private string selectedProfileName = null;      // Какой профиль сейчас выбран
    public static GameData CurrentGameData { get; private set; } // Текущие загруженные данные

    private void Start()
    {
        // Подписываемся на кнопки
        buttonCreateNew.onClick.AddListener(OnCreateNewProfileClicked);
        buttonPlay.onClick.AddListener(OnPlayClicked);

        RefreshSaveList(); // Показать все сохранения при старте
        buttonPlay.interactable = false; // Пока не выбрали профиль — нельзя играть
    }

    // ================================================
    // 1. Создать новый профиль
    // ================================================
    private void OnCreateNewProfileClicked()
    {
        string name = inputFieldNewProfile.text.Trim();

        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("Введите имя профиля!");
            return;
        }

        if (SaveSystem.GetAllSaveNames().Contains(name))
        {
            Debug.LogWarning($"Профиль с именем '{name}' уже существует!");
            // Можно добавить диалог перезаписи
            return;
        }

        SaveSystem.CreateNewSave(name);
        inputFieldNewProfile.text = ""; // Очистить поле
        RefreshSaveList();

        // Автоматически выбрать только что созданный
        SelectProfile(name);
    }

    // ================================================
    // 2. Обновить список профилей
    // ================================================
    private void RefreshSaveList()
    {
        // Очистка
        foreach (Transform child in savesListParent)
            Destroy(child.gameObject);

        var saves = SaveSystem.GetAllSaveNames();
        if (saves.Count == 0)
        {
            CreateEmptyLabel();
            return;
        }

        foreach (string saveName in saves)
        {
            GameObject slotObj = Instantiate(saveSlotPrefab, savesListParent, false);
            slotObj.name = saveName;
            var slot = slotObj.GetComponent<SaveSlotUI>();

            if (slot == null)
            {
                Debug.LogError("На префабе слота нет компонента SaveSlotUI!");
                continue;
            }

            slot.Setup(
                saveName,
                loadAction: (name) => SelectProfile(name),    // ← автоматически передаёт имя
                deleteAction: (name) => DeleteProfile(name)   // ← автоматически передаёт имя
            );
            Debug.Log(saveName);

            // Подсвечиваем текущий выбранный профиль
            slot.SetSelected(name == selectedProfileName);
        }
    }

    // ================================================
    // 3. Выбрать профиль (нажата кнопка "Загрузить")
    // ================================================
    public void SelectProfile(string profileName)
    {
        selectedProfileName = profileName;
        CurrentGameData = SaveSystem.Load(profileName);

        if (CurrentGameData != null)
        {
            Debug.Log($"Выбран профиль: {profileName} (игроков: {CurrentGameData.playerDatas.Length})");
            if (currentProfileText) currentProfileText.text = $"Текущий: {profileName}";
            buttonPlay.interactable = true;
        }
        else
        {
            Debug.LogError("Не удалось загрузить профиль!");
        }
    }

    // ================================================
    // 4. Удалить профиль
    // ================================================
    public void DeleteProfile(string profileName)
    {
        // Можно добавить диалог подтверждения
        bool deleted = SaveSystem.DeleteSave(profileName);

        if (deleted)
        {
            if (selectedProfileName == profileName)
            {
                selectedProfileName = null;
                CurrentGameData = null;
                buttonPlay.interactable = false;
                if (currentProfileText) currentProfileText.text = "Профиль не выбран";
            }
            RefreshSaveList();
        }
    }

    // ================================================
    // 5. Кнопка "Играть" — переходим в игру
    // ================================================
    private void OnPlayClicked()
    {
        if (string.IsNullOrEmpty(selectedProfileName) || CurrentGameData == null)
        {
            Debug.LogError("Профиль не выбран!");
            return;
        }

        // Здесь сохраняем имя текущего профиля, чтобы в игре знать, куда сохранять
        PlayerPrefs.SetString("LastProfile", selectedProfileName);
        PlayerPrefs.Save();

        // Переходим на игровую сцену
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle"); // ← замени на свою
    }

    // ================================================
    // Опционально: при выходе из игры очистить кэш (не обязательно)
    // ================================================
    private void OnApplicationQuit()
    {
        SaveSystem.ClearCache();
    }
    private void CreateEmptyLabel()
    {
        GameObject go = new GameObject("Нет профилей");
        go.transform.SetParent(savesListParent, false);

        var text = go.AddComponent<TextMeshProUGUI>();
        text.text = "Нет сохранённых профилей\n\nСоздайте новый, чтобы начать играть";
        text.fontSize = 28;
        text.color = new Color(0.7f, 0.7f, 0.7f);
        text.alignment = TextAlignmentOptions.Center;

        // Чтобы текст занимал всю ширину
        RectTransform rt = text.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(1, 0.5f);
        rt.sizeDelta = new Vector2(0, 100);
    }
}