using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public Unit[] units;
    public bool querySet = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        units = FindObjectsByType<Unit>(FindObjectsSortMode.None);
    }
    void Update()
    {
        if (units.Length == 6 && !querySet)
        {
            units = InitiateQuerry(units);
            querySet = true;
            units[0].active = true;
        }
        NextTurn(units);
    }
    public void NextTurn(Unit[] units)
    {
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].endTurn)
            {
                units[i].endTurn = false;
                if (i == 5)
                {
                    units[0].active = true;
                }
                else
                {
                    units[i+1].active = true;
                }                    
            }
        }
    }

    // Update is called once per frame
    
    public Unit[] InitiateQuerry(Unit[] units)
    {
        var sortedUnits = units.OrderByDescending(e => e.initiative).ToArray();
        foreach (var unit in sortedUnits)
        {
            Debug.Log($"{unit.Name}: {unit.initiative} Инициатива");
        }
        return sortedUnits;
    }
}
