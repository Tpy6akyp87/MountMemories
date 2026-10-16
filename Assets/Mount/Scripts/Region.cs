using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Region : MonoBehaviour
{
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;

    public string regionName;
    public int regionThreat;
    public TextMeshProUGUI regName;
    public TextMeshProUGUI regThreat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        LoadGameData();
        switch (regionName)
        {
            case "Deep dark hole":
                regName.text = regionName;
                regThreat.text = CurrentGameData.playerDatas.threatRegi1.ToString();
                break;
            case "Lost mines":
                regName.text = regionName;
                regThreat.text = CurrentGameData.playerDatas.threatRegi2.ToString();
                break;
            case "Hinterlands":
                regName.text = regionName;
                regThreat.text = CurrentGameData.playerDatas.threatRegi3.ToString();
                break;
        }
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
