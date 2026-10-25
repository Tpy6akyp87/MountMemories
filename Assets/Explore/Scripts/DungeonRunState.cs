using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonRunState
{
    public string dungeonId;
    public List<NodeState> nodes = new();
    public int currentNodeIndex = -1;
    public int supplies;
    public int gold;
    public List<HeroRunState> party = new();
    public List<string> flags = new();
    public bool isComplete;
    public bool isFailed;
}

[System.Serializable]
public class NodeState
{
    public int index;
    public NodeType type;
    public Vector2 mapPosition;     // для UI
    public List<int> connections;   // индексы соседних узлов
    public bool visited;
    public bool resolved;           // событие/бой отработаны
    public string payloadId;        // id события/врага, если нужно
}

[System.Serializable]
public class HeroRunState
{
    public string heroId;
    public int currentHp;
}