using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonDefinition", menuName = "Scriptable Objects/DungeonDefinition")]
public class DungeonDefinition : ScriptableObject
{
    public string dungeonId;
    public string displayName;
    [TextArea] public string description;
    public int nodeCount = 12;
    public int suppliesCost = 3;          // сколько припасов стоит вход
    public List<NodeTypeWeight> nodeWeights; // вероятности типов узлов
    public EventDefinition[] eventPool;   // пул событий
    public EnemyDefinition[] enemyPool;
    public BossDefinition boss;
    public RewardTable rewardTable;
}

[System.Serializable]
public class NodeTypeWeight
{
    public NodeType type;
    public float weight;
}

public enum NodeType { Start, Battle, Event, Treasure, Rest, Boss, Exit }
