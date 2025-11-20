using UnityEngine;

public class Enemy : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        GetStats("VRAG", 22, 12, 4);
        hpBar.SetHP(hp, maxhp);
        active = false;
        placeSprite.SetActive(false);
        endTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        CursorOnMe();
        //ImATarget();
        hpBar.SetHP(hp, maxhp);
        if (active)
        {
            placeSprite.SetActive(true);
            Action();
        }
    }
}
