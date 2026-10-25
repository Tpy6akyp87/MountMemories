using UnityEngine;

public static class DungeonGenerator
{
    public static DungeonRunState Generate(DungeonDefinition def)
    {
        var state = new DungeonRunState { dungeonId = def.dungeonId };

        // ѕроста€ слоЄна€ генераци€: N слоЄв, в каждом 1Ц3 узла,
        // св€зи только между соседними сло€ми.
        // ѕервый узел Ч Start, последний Ч Boss/Exit.

        int layers = Mathf.Max(3, def.nodeCount / 2);
        // ... строим nodes с mapPosition по сетке ...
        // ... св€зи: каждый узел сло€ i соедин€ем с 1Ц2 узлами сло€ i+1 ...
        // ... назначаем типы по weights, гарантируем Start/Boss/Exit ...

        return state;
    }
}