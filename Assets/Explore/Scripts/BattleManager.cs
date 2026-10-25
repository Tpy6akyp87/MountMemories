using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public Transform enemyRow, partyRow;
    public BattleUnitView unitPrefab;
    public Transform actionBar;
    public ActionButton actionPrefab;
    public TMP_Text turnText, battleLog;

    List<BattleUnit> units = new();
    List<BattleUnit> turnOrder = new();
    int turnIndex;
    System.Action<bool> onFinished;

    public void StartBattle(List<HeroRunState> party, EnemyDefinition[] enemies,
                            System.Action<bool> onDone)
    {
        onFinished = onDone;
        units.Clear();

        foreach (var h in party)
        {
            var def = DungeonManager.Instance.ResolveHero(h.heroId);
            units.Add(new BattleUnit(def, h, isHero: true));
        }
        foreach (var e in enemies)
            units.Add(new BattleUnit(e, isHero: false));

        BuildUnitViews();
        turnOrder = units.OrderByDescending(u => u.Speed).ToList();
        turnIndex = 0;
        BeginTurn();
    }

    void BeginTurn()
    {
        if (CheckEnd()) return;
        var u = turnOrder[turnIndex];
        turnText.text = $"Ход: {u.DisplayName}";

        if (u.IsHero) ShowActions(u);
        else          { EnemyAct(u); }
    }

    void ShowActions(BattleUnit hero)
    {
        ClearActions();
        foreach (var ability in hero.Abilities)
        {
            var btn = Instantiate(actionPrefab, actionBar);
            btn.SetLabel(ability.name);
            btn.OnClick += () => { hero.UseAbility(ability, units); EndTurn(); };
        }
    }

    void EnemyAct(BattleUnit enemy)
    {
        var target = units.Where(u => u.IsHero && u.IsAlive).OrderBy(_ => Random.value).First();
        enemy.BasicAttack(target);
        EndTurn();
    }

    void EndTurn()
    {
        if (CheckEnd()) return;
        turnIndex = (turnIndex + 1) % turnOrder.Count;
        while (!turnOrder[turnIndex].IsAlive)
            turnIndex = (turnIndex + 1) % turnOrder.Count;
        RefreshUnitViews();
        BeginTurn();
    }

    bool CheckEnd()
    {
        bool heroesAlive = units.Any(u => u.IsHero && u.IsAlive);
        bool enemiesAlive = units.Any(u => !u.IsHero && u.IsAlive);
        if (heroesAlive && enemiesAlive) return false;
        onFinished?.Invoke(heroesAlive);
        return true;
    }

    void BuildUnitViews()   { /* инстанс префабов, привязка к BattleUnit */ }
    void RefreshUnitViews() { /* обновить HP-бары, статусы */ }
    void ClearActions()     { foreach (Transform c in actionBar) Destroy(c.gameObject); }
}
