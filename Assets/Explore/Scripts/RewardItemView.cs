using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardItemView : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text labelText;

    public void Set(Sprite icon, string label)
    {
        if (iconImage != null && icon != null)
            iconImage.sprite = icon;

        if (labelText != null)
            labelText.text = label;
    }
}