using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public TMP_Text titleText, bodyText;
    public Transform choiceContainer;
    public ChoiceButton choicePrefab;
    public Image artImage;

    System.Action onFinished;

    public void Play(EventDefinition def, System.Action onDone)
    {
        onFinished = onDone;
        titleText.text = def.title;
        bodyText.text = def.body;
        if (artImage) artImage.sprite = def.art;

        ClearChoices();
        foreach (var choice in def.choices)
            SpawnChoice(choice);
    }

    void SpawnChoice(EventChoice choice)
    {
        var btn = Instantiate(choicePrefab, choiceContainer);
        btn.SetLabel(choice.label);
        btn.OnClick += () => ResolveChoice(choice);
    }

    void ResolveChoice(EventChoice choice)
    {
        bool success = true;
        if (choice.statCheck != null)
        {
            int partyStat = GetPartyStat(choice.statCheck.stat);
            int roll = Random.Range(1, 21); // d20
            success = roll + partyStat >= choice.statCheck.difficulty;
        }

        var outcomes = success ? choice.successOutcomes : choice.failOutcomes;
        foreach (var o in outcomes) ApplyOutcome(o);

        bodyText.text = choice.resultText;
        ClearChoices();
        // кнопка «Продолжить»
        var btn = Instantiate(choicePrefab, choiceContainer);
        btn.SetLabel("Продолжить");
        btn.OnClick += () => onFinished?.Invoke();
    }

    int GetPartyStat(StatType stat)
    {
        return DungeonManager.Instance.State.party
            .Select(h => DungeonManager.Instance.ResolveHero(h.heroId)) // <-- Вот так
            .Max(h => stat switch {
                StatType.Might => h.might,
                StatType.Wit => h.wit,
                StatType.Charm => h.charm,
                _ => 0
            });
    }

    void ApplyOutcome(Outcome o)
    {
        var s = DungeonManager.Instance.State;
        switch (o.type)
        {
            case OutcomeType.Gold: s.gold += o.amount; break;
            case OutcomeType.Supplies: s.supplies += o.amount; break;
            case OutcomeType.Heal:     /* вылечить отряд */ break;
            case OutcomeType.Damage:   /* ранить отряд */ break;
            case OutcomeType.Flag: s.flags.Add(o.param); break;
            case OutcomeType.Item:     /* добавить предмет */ break;
        }
    }

    void ClearChoices() { foreach (Transform c in choiceContainer) Destroy(c.gameObject); }
}
