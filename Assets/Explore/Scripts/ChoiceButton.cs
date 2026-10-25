using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceButton : MonoBehaviour
{
    public TMP_Text labelText;
    public Button button;

    public event Action OnClick;

    void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(() => OnClick?.Invoke());
    }

    public void SetLabel(string text)
    {
        if (labelText != null) labelText.text = text;
        else Debug.LogWarning("Не назначен labelText в инспекторе для ChoiceButton!");
    }
}