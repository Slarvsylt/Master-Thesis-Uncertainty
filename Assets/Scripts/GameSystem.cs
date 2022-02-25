using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

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
/// 
/// </summary>
public class GameSystem : MonoBehaviour
{
    public static GameSystem gameSystem;

    public Dictionary<Unit, StatusEffect> statusEffects;

    private void Awake()
    {
        gameSystem = this;
    }

    public IEnumerator EndOfTurnEffects()
    {
        yield return new WaitForSeconds(1);
    }

    public IEnumerator StartOfturnEffects()
    {
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Attack(Unit chosenUnit, Unit Target)
    {
        chosenUnit.PerformAttack();
        Target.TakeDamage(1);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Defend(Unit chosenUnit, Unit Target)
    {
        chosenUnit.Defend();
        Target.defended = true;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move(Unit chosenUnit, Move chosenMove)
    {
        chosenUnit.MakeMove(chosenMove);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move(Unit chosenUnit, Move chosenMove, Unit Target)
    {
        chosenUnit.MakeMove(chosenMove);
        Target.HitByMove(chosenMove);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
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
