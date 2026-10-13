using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    public string BLDName;

    [Header("Camp Ui (Заполнять ТОЛЬКО на зданиях на сцене)")]
    public Image image;
    public TextMeshProUGUI buildLvl;
    public TextMeshProUGUI buildName;
    public Button exploreButton;

    [Header("Ссылки на Общую Панель (Заполнять на зданиях на сцене)")]
    [Tooltip("Перетащите сюда общую UI-панель из Canvas, на которой тоже висит этот скрипт")]
    public Building globalPanelManager;

    
    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;

    void Start()
    {
        LoadGameData();
        switch (BLDName)
        {
            case "Blacksmith":
                buildName.text = BLDName;
                buildLvl.text = CurrentGameData.playerDatas.lvlBlaSm.ToString();
                break;
            case "Kitchen":
                buildName.text = BLDName;
                buildLvl.text = CurrentGameData.playerDatas.lvlKitchen.ToString();
                break;

            case "Weaponmaster":
                buildName.text = BLDName;
                buildLvl.text = CurrentGameData.playerDatas.lvlWeapMaster.ToString();
                break;

            case "Runemaster":
                buildName.text = BLDName;
                buildLvl.text = CurrentGameData.playerDatas.lvlRuneMaster.ToString();
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
