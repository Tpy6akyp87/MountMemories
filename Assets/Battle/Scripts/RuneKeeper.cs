using UnityEngine;

public class RuneKeeper : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetStats("Glasha", 22, 12, 3);
        hpBar.SetHP(hp, maxhp);
        active = false;
        endTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetHP(hp, maxhp);
        if (active)
        {
            Action();
        }
    }
}
