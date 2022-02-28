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
    public int MaxTurns;
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

    public Dictionary<Unit, List<StatusEffect>> statusEffects;

    public delegate void OnBattleMenuSelectionCallback();
    public OnBattleMenuSelectionCallback onBattleMenuSelectionCallback;
    public delegate void OnBattleSelectionModeConfirmCallback();
    public OnBattleSelectionModeConfirmCallback onBattleSelectionModeConfirmCallback;

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

    public void IncreaseEffectsTurnCounter()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].TurnsSinceApplied++;
                if (entry.Value[i].TurnsSinceApplied >= entry.Value[i].MaxTurns)
                {
                    entry.Value.RemoveAt(i);
                }
            }
        }
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
