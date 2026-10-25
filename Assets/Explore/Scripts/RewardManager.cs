using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public TMP_Text title;
    public Transform container;
    public RewardItemView itemPrefab;
    public Button continueButton;

    System.Action onContinue;

    public void Show(RewardBundle bundle, System.Action onDone)
    {
        onContinue = onDone;
        Clear();
        foreach (var r in bundle.items)
        {
            var v = Instantiate(itemPrefab, container);
            v.Set(r.icon, $"{r.label} x{r.amount}");
        }
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() => { Clear(); onContinue?.Invoke(); });
    }

    void Clear() { foreach (Transform c in container) Destroy(c.gameObject); }
}
