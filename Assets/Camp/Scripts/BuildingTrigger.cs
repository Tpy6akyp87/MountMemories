using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuildingTrigger : MonoBehaviour
{
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;

    public string buildName;
    public string nextscene;
    //public GameObject infoPanel;
    public Image panelImage;
    public Image gradeImage;
    public Button closeButton;
    public TextMeshProUGUI npsBaseText;
    public TextMeshProUGUI upgradeText;
    public Button upgradeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadGameData();
       // buildName = GetComponentInParent<Building>().BLDName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPanel(string name)
    {
        buildName= name;
        switch (name)
        {
            case "Blacksmith":
                npsBaseText.text = CurrentGameData.playerDatas.blasmBaseText;
                upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.blasmName + " collect for me " + CurrentGameData.playerDatas.upCostBmGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostBmGold.ToString() + " gold!";
                break;

            case "Kitchen":
                npsBaseText.text = CurrentGameData.playerDatas.kitchenBaseText;
                upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.kitchenName + " collect for me " + CurrentGameData.playerDatas.upCostKitGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostKitGold.ToString() + " gold!";
                break;

            case "Weaponmaster":
                npsBaseText.text = CurrentGameData.playerDatas.weapBaseText;
                upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.weapName + " collect for me " + CurrentGameData.playerDatas.upCostWeapGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostWeapGold.ToString() + " gold!";
                break;

            case "Runemaster":
                npsBaseText.text = CurrentGameData.playerDatas.runeBaseText;
                upgradeText.text = "If you want upgrade " + CurrentGameData.playerDatas.runemasterName + " collect for me " + CurrentGameData.playerDatas.upCostRuneGems.ToString() + " Gems and " + CurrentGameData.playerDatas.upCostRuneGold.ToString() + " gold!";
                break;

            default:
                Debug.LogWarning($"«дание '{buildName}' не найдено в switch!");
                break;
        }
        gameObject.SetActive(true);        
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
        foreach (Building building in buildings)
        {
            building.Refresh();
        }
    }
    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextscene);
    }

    public void UpgradeButton()
    {
        Debug.Log("Try to Upgrade " + buildName);
        switch (buildName)
        {
            case "Blacksmith":
                CurrentGameData.playerDatas.lvlBlaSm++;
                CurrentGameData.playerDatas.gems -= CurrentGameData.playerDatas.upCostBmGems;
                CurrentGameData.playerDatas.gold -= CurrentGameData.playerDatas.upCostBmGold;
                break;
            case "Kitchen":
                CurrentGameData.playerDatas.lvlKitchen++;
                CurrentGameData.playerDatas.gems -= CurrentGameData.playerDatas.upCostKitGems;
                CurrentGameData.playerDatas.gold -= CurrentGameData.playerDatas.upCostKitGold;
                break;

            case "Weaponmaster":
                CurrentGameData.playerDatas.lvlWeapMaster++;
                CurrentGameData.playerDatas.gems -= CurrentGameData.playerDatas.upCostWeapGems;
                CurrentGameData.playerDatas.gold -= CurrentGameData.playerDatas.upCostWeapGold;
                break;

            case "Runemaster":
                CurrentGameData.playerDatas.lvlRuneMaster++;
                CurrentGameData.playerDatas.gems -= CurrentGameData.playerDatas.upCostRuneGems;
                CurrentGameData.playerDatas.gold -= CurrentGameData.playerDatas.upCostRuneGold;
                break;
        }
        Debug.Log("gems = "+CurrentGameData.playerDatas.gems + ", gold = " + CurrentGameData.playerDatas.gold);
        SaveGameData();
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
