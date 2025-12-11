using UnityEngine;

public class GDataMb : MonoBehaviour
{
    public string loadingGame;
    public static GameData CurrentGameData { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Старт сейвоф");
        CurrentGameData = SaveSystem.Load(loadingGame);
        DontDestroyOnLoad(gameObject);
        //var saves = SaveSystem.GetAllSaveNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
