using NUnit.Framework.Internal.Builders;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Fighter : TeamUnit
{
    public string stAbilName;
    public string ndAbilName;
    public Button stAbil;    
    public Button ndAbil;
    [SerializeField]
    public Sprite firstAbilImage;
    public Sprite secondAbilImage;
    public Sprite thirdAbilImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //LoadStats();
        stAbilName = "Swing";
        ndAbilName = "Throw";
        GetStats("Dolboeb",22,12, 6);
        hpBar.SetHP(hp, maxhp);
        active = false;
        placeSprite.SetActive(false);
        endTurn = false;
        abilityNum = 0;
        //abilPanel = FindAnyObjectByType<AbilPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetHP(hp, maxhp);

        if (active)
        {
            animator.SetBool("WarIdle", true);
            //SetAbiliesOnButton();
            abilPanel.GetActiveTeamUnit(firstAbilImage, secondAbilImage, thirdAbilImage);
            placeSprite.SetActive(true);
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Unit target = hit.collider.GetComponent<Unit>();
                    Debug.Log(" ÎËÍÌÛÎË ÔÓ: " + target.name);
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
        else
        {
            animator.SetBool("WarIdle", true);
            animator.SetBool("WarAttack", false);
        }
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Action();
        }
    }




























    public void SetAbiliesOnButton() // Õ≈ »—œŒÀ‹«”≈“—ﬂ œŒ ¿
    {
        TMP_Text button1Text = stAbil.GetComponentInChildren<TMP_Text>();
        button1Text.text = stAbilName;
        stAbil.image.sprite = firstAbilImage;
        TMP_Text button2Text = ndAbil.GetComponentInChildren<TMP_Text>();
        button2Text.text = ndAbilName;
        ndAbil.image.sprite = secondAbilImage;
    }

    public void StSkill (Unit target)
    {
        animator.SetBool("WarIdle", false);
        animator.SetBool("WarAttack", true);
        target.TakeDamage(damage);
        //EndTurn();
    }
    public void NdSkill(Unit target)
    {
        animator.SetBool("WarIdle", false);
        animator.SetBool("WarAttack", true);
        target.TakeDamage(damage);
        //EndTurn();
    }

}
