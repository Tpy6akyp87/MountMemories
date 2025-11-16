using NUnit.Framework.Internal.Builders;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Rogue : Unit
{
    public string stAbilName;
    public string ndAbilName;
    public Button stAbil;
    public Button ndAbil;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ndAbilName = "StoneShot";
        stAbilName = "Garrote";
        GetStats("Ignat", 22, 12, 5);
        hpBar.SetHP(hp, maxhp);
        active = false;
        placeSprite.SetActive(false);
        endTurn = false;
        abilityNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetHP(hp, maxhp);
        if (active)
        {
            SetAbiliesOnButton();
            placeSprite.SetActive(true);
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Unit target = hit.collider.GetComponent<Unit>();
                    Debug.Log("Кликнули по: " + target.name);
                    if (abilityNum == 1)
                    {
                        StSkill(target);
                    }
                    if (abilityNum == 2)
                    {
                        NdSkill(target);
                    }
                }
            }

            //Action();
        }
    }
    public void SetAbiliesOnButton()
    {
        TMP_Text button1Text = stAbil.GetComponentInChildren<TMP_Text>();
        button1Text.text = stAbilName;
        TMP_Text button2Text = ndAbil.GetComponentInChildren<TMP_Text>();
        button2Text.text = ndAbilName;
    }
    public void StSkill(Unit target)
    {
        target.TakeDamage(damage);
        endTurn = true;
        active = false;
        placeSprite.SetActive(false);
    }
    public void NdSkill(Unit target)
    {
        target.TakeDamage(damage);
        endTurn = true;
        active = false;
        placeSprite.SetActive(false);
    }
}
