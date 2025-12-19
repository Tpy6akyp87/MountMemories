using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    public string BLDName;

    [Header ("Camp Ui")]
    public Image image;
    public TextMeshProUGUI buildLvl;
    public TextMeshProUGUI buildName;
    public Button exploreButton;
    //public Button upgradeButton;
    public GameObject panel;

    [Header("Explore Ui")]
    public Image panelImage;
    public Image gradeImage;
    public Button closeButton;
    public TextMeshProUGUI npsBaseText;
    public TextMeshProUGUI upgradeText;
    public Button upgradeButton;

    bool openPanel;
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadGameData();
        panel.SetActive (false);
        openPanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explore()
    {
        if (!openPanel) { panel.SetActive(true); openPanel = true; }
        else { panel.SetActive(false); openPanel = false; }
    }
    public void LoadGameData()
    {
        gDataMb = FindAnyObjectByType<GDataMb>();
        CurrentGameData = SaveSystem.Load(gDataMb.loadingGame);
        switch (BLDName)
        {
            case "Blacksmith":
                {
                    buildName.text = CurrentGameData.playerDatas.blasmName;
                    buildLvl.text = CurrentGameData.playerDatas.lvlBlaSm.ToString();
                    npsBaseText.text = CurrentGameData.playerDatas.blasmBaseText;
                    upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.blasmName + " collect for me " + CurrentGameData.playerDatas.upCostBmGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostBmGold.ToString() + " gold!";
                }
                break;
        }
    }
}
