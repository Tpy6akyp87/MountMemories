using NUnit.Framework.Internal.Builders;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class AbilPanel : MonoBehaviour
{
    public string type;

    public Button firstAbilbutt;
    public Button secondAbilbutt;
    public Button thirdAbilbutt;
    public Button jumpbutt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetActiveTeamUnit(Sprite firstAbil, Sprite secondAbil, Sprite thirdAbil)
    {
        firstAbilbutt.image.sprite = firstAbil;
        secondAbilbutt.image.sprite = secondAbil;
        thirdAbilbutt.image.sprite = thirdAbil;
    }
}
