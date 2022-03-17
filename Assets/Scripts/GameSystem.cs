using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Dictionary<Unit, List<StatusEffect>> statusEffects;

    public List<EnemyButton> unitsUI;
    public List<EnemyButton> enemiesUI;

    private void Awake()
    {
        gameSystem = this;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
        PopUpTextController.Initialize();
    }

    private void Start()
    {
        statusEffects = new Dictionary<Unit, List<StatusEffect>>();
    }

    public void ApplyStatusEffect(Unit unit, Effect effect)
    {
        StatusEffect sf = new StatusEffect();
        sf.effect = effect;
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
            Debug.Log("StatusEffect: \n Turns:" + sf.TurnsSinceApplied + "\n Max: " + sf.effect.maxTurns + "\n Effect: " + sf.effect.EffectName);
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

        Debug.Log("StatusEffect: \n Turns:" + sf.TurnsSinceApplied + "\n Max: " + sf.effect.maxTurns + "\n Effect: " + sf.effect.EffectName);

        sf.effect.affected = unit;
        sf.effect.OnInflict();
        list.Add(sf);
        statusEffects.Remove(unit);
        statusEffects.Add(unit, list);
    }

    public void EndOfTurnEffects()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].effect.OnTurnEnd();
            }
        }
    }

    public void StartOfturnEffects()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].effect.OnTurnBegin();
            }
        }
    }

    public void IncreaseEffectsTurnCounter()
    {
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].TurnsSinceApplied++;
                if (entry.Value[i].TurnsSinceApplied >= entry.Value[i].effect.maxTurns)
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
        if (RandomChance(chosenUnit.hitMod * 0.9f))
        {
            float damage = 1 * chosenUnit.damageMod;
            yield return StartCoroutine(DamageUnit(Target.index, damage));
            Debug.Log("Damage!");
            Target.TakeDamage(damage);
            //Debug.Log(chosenUnit.Name + " atacc " + Target.Name);
        }
        else
        {
            //Miss and do something else
            //Debug.Log(chosenUnit.Name + " atacc " + Target.Name + " but MISSES!");
        }
    }

    public IEnumerator Defend(Unit chosenUnit, Unit Target)
    {
        chosenUnit.Defend();
        Target.defended = true;
        yield return new WaitForSeconds(1);
    }

    public void Move(Unit chosenUnit, Move chosenMove, Unit Target)
    {
        //Debug.Log("Move!!");
        chosenUnit.MakeMove(chosenMove);
        if (RandomChance(0.9f * chosenUnit.hitMod))
        {
            Target.HitByMove(chosenMove);

            foreach (Effect effect in chosenMove.Effects)
            {
                Debug.Log(effect.EffectName);
                ApplyStatusEffect(Target, effect);
            }
        }
        else
        {
            //missed and do something else
        }
    }

    public IEnumerator DamageUnit(int index, float dam)
    {
        Debug.Log("DamageUnit");
        PopUpTextController.CreatePopUpText(dam.ToString(),enemiesUI[index].transform);
        yield return StartCoroutine(enemiesUI[index].Shake());
    }

    public IEnumerator DamageFriendlyUnit(int index, float dam)
    {
        Debug.Log("DamageUnit");
        PopUpTextController.CreatePopUpText(dam.ToString(), unitsUI[index].transform);
        yield return StartCoroutine(unitsUI[index].Shake());
    }

    public bool RandomChance(float chance)
    {
        return UnityEngine.Random.value <= chance;
    }
}
