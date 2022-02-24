using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Used for storing data about applied effects.
/// </summary>
public class StatusEffect
{
    public int TurnsSinceApplied;
    public Effect effect;
}

/// <summary>
/// Handles attacks, defends, and moves made by units.
/// Listens for actions made by units and player.
/// Remembers which status effects are applied to which units and for how long.
/// </summary>
public class GameSystem : MonoBehaviour
{
    public static GameSystem gameSystem;

    public Dictionary<Unit, StatusEffect> statusEffects;

    private void Awake()
    {
        gameSystem = this;
    }

    public event Action OnMove;
    public void Move()
    {
        OnMove();
    }

    public event Action OnAttack;
    public void Attack()
    {
        OnAttack();
    }

    public event Action OnDefend;
    public void Defend()
    {
        OnDefend();
    }

    public event Action OnEndTurn;
    public void EndTurn()
    {
        OnEndTurn();
    }

    public event Action OnStartTurn;
    public void StartTurn()
    {
        OnStartTurn();
    }
}
