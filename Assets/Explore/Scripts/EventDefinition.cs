using UnityEngine;

[CreateAssetMenu(fileName = "EventDefinition", menuName = "Scriptable Objects/EventDefinition")]
public class EventDefinition : ScriptableObject
{
    public string eventId;
    public string title;
    [TextArea] public string body;
    public Sprite art;                     // опционально
    public EventChoice[] choices;
    public string[] requiredFlags;         // показывать только если флаги есть
    public string[] setFlagsAfter;         // что выставить после
}

[System.Serializable]
public class EventChoice
{
    public string label;
    [TextArea] public string resultText;
    public StatCheck statCheck;            // может быть null = автоуспех
    public Outcome[] successOutcomes;
    public Outcome[] failOutcomes;
}

[System.Serializable]
public class StatCheck
{
    public StatType stat;                  // Might / Wit / Charm
    public int difficulty;                 // порог
}

[System.Serializable]
public class Outcome
{
    public OutcomeType type;               // Gold, Supplies, Heal, Damage, Flag, Item
    public string param;
    public int amount;
}

public enum StatType { Might, Wit, Charm }
public enum OutcomeType { Gold, Supplies, Heal, Damage, Flag, Item }
