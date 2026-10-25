using UnityEngine;

public class SaveBridge : MonoBehaviour
{
    public void SaveDungeon(DungeonRunState state)
    {
        // сериализуем в JSON и пишем в твой слот сохранения
        string json = JsonUtility.ToJson(state);
        // твой существующий SaveSystem: SaveSystem.SetDungeonState(json);
    }

    public DungeonRunState LoadDungeon()
    {
        string json = null; // SaveSystem.GetDungeonState();
        return string.IsNullOrEmpty(json) ? null : JsonUtility.FromJson<DungeonRunState>(json);
    }
}
