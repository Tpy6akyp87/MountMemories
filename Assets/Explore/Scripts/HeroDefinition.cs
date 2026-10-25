using UnityEngine;

[CreateAssetMenu(fileName = "HeroDefinition", menuName = "Scriptable Objects/HeroDefinition")]
public class HeroDefinition : ScriptableObject
{
    public string heroId;
    public string displayName;
    public Sprite portrait;
    public int maxHp;
    public int might;    // сила
    public int wit;      // ум
    public int charm;    // обаяние
    public HeroAbility[] abilities;
}
