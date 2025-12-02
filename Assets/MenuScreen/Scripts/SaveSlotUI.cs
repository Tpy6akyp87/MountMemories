using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI profileNameText;
    public Button loadButton;
    public Button deleteButton;

    // Эти действия назначит MainMenu
    private string profileName;
    private System.Action<string> onLoadClicked;
    private System.Action<string> onDeleteClicked;

    // Вызывается из MainMenu при создании слота
    public void Setup(string name, System.Action<string> loadAction, System.Action<string> deleteAction)
    {
        profileName = name;

        if (profileNameText) profileNameText.text = name;

        if (loadButton) loadButton.onClick.AddListener(() => loadAction?.Invoke(profileName));
        if (deleteButton) deleteButton.onClick.AddListener(() => deleteAction?.Invoke(profileName));
    }

    // Опционально: подсветка выбранного профиля
    public void SetSelected(bool selected)
    {
        // Например, поменять цвет фона
        var img = GetComponent<Image>();
        if (img) img.color = selected ? new Color(0.2f, 0.6f, 1f, 0.3f) : new Color(1, 1, 1, 0.1f);
    }
}