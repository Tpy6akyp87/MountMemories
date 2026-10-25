using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Dungeon/Enemy")]
public class EnemyDefinition : ScriptableObject
{
    public string enemyId;
    public string displayName;
    public Sprite icon;
    public int maxHp;
    public int attack;
    public int defense;
    public EnemyAbility[] abilities;
}

// Добавь это в самый низ файла:
[System.Serializable]
public class EnemyAbility
{
    public string name;
    public int damage;
    // Сюда потом можно добавить эффекты, лечение и т.д.
}