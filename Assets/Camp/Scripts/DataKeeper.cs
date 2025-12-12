using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataKeeper : MonoBehaviour
{
    [Header("Team")]
    public TextMeshProUGUI warName;
    public TextMeshProUGUI warHP;
    public TextMeshProUGUI warDmg;

    public TextMeshProUGUI runeName;
    public TextMeshProUGUI runeHP;
    public TextMeshProUGUI runeDmg;

    public TextMeshProUGUI rogName;
    public TextMeshProUGUI rogHP;
    public TextMeshProUGUI rogDmg;

    [Header("Camp")]
    public TextMeshProUGUI lvlBlaSm;

    public static GameData CurrentGameData { get; private set; }
    public GDataMb gDataMb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadGameData();
        warName.text = CurrentGameData.playerDatas.warName;
        warHP.text = "HP " + CurrentGameData.playerDatas.warHP.ToString();
        warDmg.text = "Damage " + CurrentGameData.playerDatas.warDamage.ToString();

        runeName.text = CurrentGameData.playerDatas.runeName;
        runeHP.text = "HP " + CurrentGameData.playerDatas.runeHP.ToString();
        runeDmg.text = "Power " + CurrentGameData.playerDatas.runeDamage.ToString();

        rogName.text = CurrentGameData.playerDatas.rogName;
        rogHP.text = "HP " + CurrentGameData.playerDatas.rogHP.ToString();
        rogDmg.text = "Damage " + CurrentGameData.playerDatas.rogDamage.ToString();


        lvlBlaSm.text = CurrentGameData.playerDatas.lvlBlaSm.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGameData()
    {
        gDataMb = FindAnyObjectByType<GDataMb>();
        CurrentGameData = SaveSystem.Load(gDataMb.loadingGame);
    }
}
