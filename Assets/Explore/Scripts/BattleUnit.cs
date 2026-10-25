using System.Collections.Generic;
using UnityEngine;

public class BattleUnit
{
    public string DisplayName { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Speed { get; private set; } // Для инициативы
    public bool IsHero { get; private set; }

    public List<HeroAbility> Abilities { get; private set; }

    public bool IsAlive => CurrentHp > 0;

    // Конструктор для Героя
    public BattleUnit(HeroDefinition def, HeroRunState state, bool isHero)
    {
        IsHero = isHero;
        DisplayName = def.displayName;
        MaxHp = def.maxHp;
        CurrentHp = state.currentHp; // Берем текущее HP из сохранения
        Attack = def.might;          // Упрощенно: сила = атака
        Defense = def.wit;           // Упрощенно: ум = защита
        Speed = def.charm;           // Упрощенно: обаяние = скорость
        Abilities = new List<HeroAbility>(); // Пока пустой, добавишь позже
    }

    // Конструктор для Врага
    public BattleUnit(EnemyDefinition def, bool isHero)
    {
        IsHero = isHero;
        DisplayName = def.displayName;
        MaxHp = def.maxHp;
        CurrentHp = def.maxHp;
        Attack = def.attack;
        Defense = def.defense;
        Speed = 5; // Заглушка, если у врага нет скорости
        Abilities = new List<HeroAbility>();
    }

    public void TakeDamage(int damage)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - damage);
    }

    public void BasicAttack(BattleUnit target)
    {
        int damage = Mathf.Max(1, Attack - target.Defense + Random.Range(-2, 3));
        target.TakeDamage(damage);
        Debug.Log($"{DisplayName} ударил {target.DisplayName} на {damage} урона!");
    }

    public void UseAbility(HeroAbility ability, List<BattleUnit> allUnits)
    {
        // Здесь будет логика способностей. Пока просто базовая атака:
        var target = allUnits.FindAll(u => !u.IsHero && u.IsAlive)[0];
        BasicAttack(target);
    }
}

// Заглушка для способностей (создай в HeroDefinition.cs)
[System.Serializable]
public class HeroAbility
{
    public string name;
    // public int damage; и т.д.
}