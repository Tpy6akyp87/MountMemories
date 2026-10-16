using UnityEngine;

public class Toinstance : MonoBehaviour
{
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;

    public string regionName;
    public int regionThreat;
    public string nextscene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextscene);
    }
    public void LoadGameData()
    {
        if (gDataMb == null) gDataMb = FindAnyObjectByType<GDataMb>();
        if (gDataMb != null)
        {
            CurrentGameData = SaveSystem.Load(gDataMb.loadingGame);
        }
    }
    public void SaveGameData()
    {
        if (gDataMb == null) gDataMb = FindAnyObjectByType<GDataMb>();
        if (gDataMb != null)
        {
            SaveSystem.Save(gDataMb.loadingGame, CurrentGameData);
        }
    }
}
