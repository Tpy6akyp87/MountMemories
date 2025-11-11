using UnityEngine;

public class Rogue : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetStats("Ignat", 22, 12);
        hpBar.SetHP(hp, maxhp);
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetHP(hp, maxhp);
    }
}
