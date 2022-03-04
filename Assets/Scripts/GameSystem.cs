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
    public LayerMask enemiesLayer;
    public LayerMask friendsLayer;

    private void Awake()
    {
        gameSystem = this;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
    }

    private void Start()
    {
        statusEffects = new Dictionary<Unit, List<StatusEffect>>();
    }

    public void ApplyStatusEffect(Unit unit, Effect effect)
    {
        StatusEffect sf = new StatusEffect();
        sf.effect = effect;
        sf.MaxTurns = 2;
        sf.TurnsSinceApplied = 0;

        List<StatusEffect> list;

        if(!statusEffects.TryGetValue(unit, out list))
        {
            //No effects yet
            list = new List<StatusEffect>();
            statusEffects.Remove(unit);
            sf.effect.affected = unit;
            sf.effect.OnInflict();
            list.Add(sf);
            statusEffects.Add(unit, list);
            Debug.Log("StatusEffect: \n Turns:" + sf.TurnsSinceApplied + "\n Max: " + sf.MaxTurns + "\n Effect: " + sf.effect.EffectName);
            return;
        }

        foreach(StatusEffect s in list)
        {
            if(s.effect == sf.effect)
            {
                //Already exist
                return;
            }
        }

        Debug.Log("StatusEffect: \n Turns:" + sf.TurnsSinceApplied + "\n Max: " + sf.MaxTurns + "\n Effect: " + sf.effect.EffectName);

        sf.effect.affected = unit;
        sf.effect.OnInflict();
        list.Add(sf);
        statusEffects.Remove(unit);
        statusEffects.Add(unit, list);
    }

    public IEnumerator EndOfTurnEffects()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].effect.OnTurnEnd();
            }
        }
        yield return new WaitForSeconds(1);
    }

    public IEnumerator StartOfturnEffects()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].effect.OnTurnBegin();
            }
        }
        yield return new WaitForSeconds(1);
    }

    public void IncreaseEffectsTurnCounter()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].TurnsSinceApplied++;
                Debug.Log("Increasing turn counter for effect: " + entry.Value[i].effect.EffectName + " " + i);
                if (entry.Value[i].TurnsSinceApplied >= entry.Value[i].MaxTurns)
                {
                    Debug.Log("Removed effect: "+ entry.Value[i].effect.EffectName);
                    entry.Value[i].effect.OnRemoved();
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

    public void Move(Unit chosenUnit, Move chosenMove, Unit Target)
    {
        //Debug.Log("Move!!");
        chosenUnit.MakeMove(chosenMove);
        Target.HitByMove(chosenMove);

        foreach(Effect effect in chosenMove.Effects)
        {
            Debug.Log(effect.EffectName);
            ApplyStatusEffect(Target, effect);
        }
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
