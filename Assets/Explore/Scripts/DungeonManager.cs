using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    [Header("Data")]
    public DungeonDefinition dungeon;
    public HeroDefinition[] allHeroes;          // для резолва по id

    [Header("UI Panels")]
    public GameObject mapPanel;
    public GameObject eventPanel;
    public GameObject battlePanel;
    public GameObject rewardPanel;
    public Transform nodeContainer;
    public Transform lineContainer;
    public NodeView nodePrefab;
    public LineView linePrefab;

    [Header("Systems")]
    public EventManager eventManager;
    public BattleManager battleManager;
    public RewardManager rewardManager;
    public SaveBridge saveBridge;

    public DungeonRunState State { get; private set; }
    public DungeonPhase Phase { get; private set; }

    void Awake() { Instance = this; }

    public void StartDungeon(DungeonDefinition def)
    {
        dungeon = def;
        State = DungeonGenerator.Generate(def);
        Phase = DungeonPhase.MapView;
        BuildMapUI();
        ShowPanel(mapPanel);
        saveBridge.SaveDungeon(State);
    }

    // --- Карта ---
    void BuildMapUI()
    {
        // очистить контейнеры
        // для каждого узла создать NodeView, задать позицию/иконку, подписать на клик
        // для каждой связи создать LineView
    }

    public void OnNodeClicked(int nodeIndex)
    {
        if (Phase != DungeonPhase.MapView) return;
        if (!IsReachable(nodeIndex)) return;

        State.currentNodeIndex = nodeIndex;
        var node = State.nodes[nodeIndex];
        node.visited = true;

        switch (node.type)
        {
            case NodeType.Battle:
            case NodeType.Boss:
                StartBattle(node);
                break;
            case NodeType.Event:
                StartEvent(node);
                break;
            case NodeType.Treasure:
                StartTreasure(node);
                break;
            case NodeType.Rest:
                StartRest(node);
                break;
            case NodeType.Exit:
                CompleteDungeon();
                break;
        }
    }

    bool IsReachable(int nodeIndex)
    {
        if (State.currentNodeIndex < 0) return nodeIndex == 0; // стартовый
        var cur = State.nodes[State.currentNodeIndex];
        return cur.connections.Contains(nodeIndex);
    }

    // --- Событие ---
    void StartEvent(NodeState node)
    {
        Phase = DungeonPhase.EventView;
        ShowPanel(eventPanel);
        var def = dungeon.eventPool.FirstOrDefault(e => e.eventId == node.payloadId)
                  ?? dungeon.eventPool[Random.Range(0, dungeon.eventPool.Length)];
        eventManager.Play(def, OnEventFinished);
    }

    void OnEventFinished()
    {
        State.nodes[State.currentNodeIndex].resolved = true;
        ReturnToMap();
    }

    // --- Бой ---
    void StartBattle(NodeState node)
    {
        Phase = DungeonPhase.BattleView;
        ShowPanel(battlePanel);
        var enemies = node.type == NodeType.Boss
            ? new[] { dungeon.boss.enemy }
            : PickEnemies();
        battleManager.StartBattle(State.party, enemies, OnBattleFinished);
    }

    void OnBattleFinished(bool victory)
    {
        if (!victory)
        {
            State.isFailed = true;
            saveBridge.SaveDungeon(State);
            // показать экран поражения, вернуть в лагерь
            return;
        }
        State.nodes[State.currentNodeIndex].resolved = true;
        StartReward(GenerateReward());
    }

    // --- Награда ---
    void StartReward(RewardBundle bundle)
    {
        Phase = DungeonPhase.RewardView;
        ShowPanel(rewardPanel);
        rewardManager.Show(bundle, () => ReturnToMap());
        ApplyReward(bundle);
    }
    public HeroDefinition ResolveHero(string heroId)
    {
        if (allHeroes == null)
        {
            Debug.LogError("Массив allHeroes пуст в DungeonManager!");
            return null;
        }

        foreach (var hero in allHeroes)
        {
            if (hero.heroId == heroId) return hero;
        }

        Debug.LogError($"Герой с ID {heroId} не найден!");
        return null;
    }

    // --- Прочее ---
    void StartTreasure(NodeState node) { /* сразу Reward, без боя */ }
    void StartRest(NodeState node) { /* Heal отряда, потом ReturnToMap */ }
    void ReturnToMap() { Phase = DungeonPhase.MapView; ShowPanel(mapPanel); saveBridge.SaveDungeon(State); }
    void CompleteDungeon() { State.isComplete = true; saveBridge.SaveDungeon(State); /* вернуть в лагерь */ }

    void ShowPanel(GameObject active)
    {
        mapPanel.SetActive(active == mapPanel);
        eventPanel.SetActive(active == eventPanel);
        battlePanel.SetActive(active == battlePanel);
        rewardPanel.SetActive(active == rewardPanel);
    }
    private EnemyDefinition[] PickEnemies()
    {
        if (dungeon.enemyPool == null || dungeon.enemyPool.Length == 0)
        {
            Debug.LogError("В DungeonDefinition не назначены враги (enemyPool)!");
            return new EnemyDefinition[0];
        }

        // Берем 1-2 случайных врага
        int count = UnityEngine.Random.Range(1, 3);
        EnemyDefinition[] result = new EnemyDefinition[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = dungeon.enemyPool[UnityEngine.Random.Range(0, dungeon.enemyPool.Length)];
        }
        return result;
    }

    // Временно генерируем простую награду
    private RewardBundle GenerateReward()
    {
        return new RewardBundle
        {
            items = new System.Collections.Generic.List<RewardItem>
            {
                new RewardItem { label = "Золото", amount = 50 }, // Иконку добавишь потом
                new RewardItem { label = "Припасы", amount = 2 }
            }
        };
    }

    // Применяем награду к состоянию забега
    private void ApplyReward(RewardBundle bundle)
    {
        // Временно просто добавляем золото и припасы (логику потом расширишь)
        State.gold += 50;
        State.supplies += 2;
    }
}

public enum DungeonPhase { Idle, MapView, EventView, BattleView, RewardView, Complete }

[System.Serializable]
public class RewardBundle
{
    public System.Collections.Generic.List<RewardItem> items;
}

[System.Serializable]
public class RewardItem
{
    public string label;
    public int amount;
    public UnityEngine.Sprite icon;
}