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
            case "Kitchen":
                {
                    buildName.text = CurrentGameData.playerDatas.kitchenName;
                    buildLvl.text = CurrentGameData.playerDatas.lvlKitchen.ToString();
                    npsBaseText.text = CurrentGameData.playerDatas.kitchenBaseText;
                    upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.kitchenName + " collect for me " + CurrentGameData.playerDatas.upCostKitGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostKitGold.ToString() + " gold!";
                }
                break;
            case "Weaponmaster":
                {
                    buildName.text = CurrentGameData.playerDatas.weapName;
                    buildLvl.text = CurrentGameData.playerDatas.lvlWeapMaster.ToString();
                    npsBaseText.text = CurrentGameData.playerDatas.weapBaseText;
                    upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.weapName + " collect for me " + CurrentGameData.playerDatas.upCostWeapGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostWeapGold.ToString() + " gold!";
                }
                break;
            case "Runemaster":
                {
                    buildName.text = CurrentGameData.playerDatas.runemasterName;
                    buildLvl.text = CurrentGameData.playerDatas.lvlRuneMaster.ToString();
                    npsBaseText.text = CurrentGameData.playerDatas.runeBaseText;
                    upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.runemasterName + " collect for me " + CurrentGameData.playerDatas.upCostRuneGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostRuneGold.ToString() + " gold!";
                }
                break;

        }
    }
}
