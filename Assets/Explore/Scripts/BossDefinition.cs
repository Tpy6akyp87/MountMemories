using UnityEngine;

[CreateAssetMenu(fileName = "BossDefinition", menuName = "Dungeon/Boss Definition")]
public class BossDefinition : ScriptableObject
{
    public string bossName;
    public EnemyDefinition enemy; // —ам враг, с которым будет бой
    // —юда потом можно добавить уникальные награды за босса
}