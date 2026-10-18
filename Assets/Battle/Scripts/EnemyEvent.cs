using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;
    public int threat;
    public Team team;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadGameData();
        threat = CurrentGameData.playerDatas.lvlOfThreat;
        team = FindAnyObjectByType<Team>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy event arrived");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
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
