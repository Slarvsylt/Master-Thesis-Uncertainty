using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using TMPro;

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

    public TextMeshProUGUI StatusText;

    public List<EnemyButton> unitsUI;
    public List<EnemyButton> enemiesUI;

    public DiceVis dice;

    public float GodOfFortune = 0.0f;
    private RandomMeter sanity;

    private void Awake()
    {
        gameSystem = this;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 35;
        PopUpTextController.Initialize();
    }

    private void Start()
    {
        statusEffects = new Dictionary<Unit, List<StatusEffect>>();
        sanity = GameObject.Find("Sanity").GetComponent<RandomMeter>();
    }

    public IEnumerator ApplyStatusEffect(Unit unit, Effect effect)
    {
        StatusText.text = "Applying status effects...";
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
            yield return StartCoroutine(sf.effect.OnInflict());
            list.Add(sf);
            statusEffects.Add(unit, list);
            //Debug.Log("StatusEffect: \n Turns:" + sf.TurnsSinceApplied + "\n Max: " + sf.effect.maxTurns + "\n Effect: " + sf.effect.EffectName);
            yield break;
        }

        foreach(StatusEffect s in list)
        {
            if(s.effect == sf.effect)
            {
                //Already exist
                yield break;
            }
        }

        //Debug.Log("StatusEffect: \n Turns:" + sf.TurnsSinceApplied + "\n Max: " + sf.effect.maxTurns + "\n Effect: " + sf.effect.EffectName);

        sf.effect.affected = unit;
        yield return StartCoroutine(sf.effect.OnInflict());
        list.Add(sf);
        statusEffects.Remove(unit);
        statusEffects.Add(unit, list);
    }

    public IEnumerator EndOfTurnEffects()
    {
        StatusText.text = "Triggering end of turn effects...";
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                yield return StartCoroutine(entry.Value[i].effect.OnTurnEnd());
               // yield return new WaitForSeconds(0.1f);
            }
        }
        yield break;
    }

    public IEnumerator StartOfturnEffects()
    {
        StatusText.text = "Triggering start of turn effects";
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                yield return StartCoroutine(entry.Value[i].effect.OnTurnBegin());
               // yield return new WaitForSeconds(0.1f);
            }
        }
        yield break;
    }

    public IEnumerator IncreaseEffectsTurnCounter()
    {
        StatusText.text = "Removing effects due for removal...";
        foreach (KeyValuePair<Unit, List<StatusEffect>> entry in statusEffects)
        {
            for (int i = entry.Value.Count - 1; i >= 0; i--)
            {
                entry.Value[i].TurnsSinceApplied++;
                if (entry.Value[i].TurnsSinceApplied >= entry.Value[i].effect.maxTurns)
                {
                    Debug.Log("Removed effect: "+ entry.Value[i].effect.EffectName);
                    yield return StartCoroutine(entry.Value[i].effect.OnRemoved());
                    entry.Value.RemoveAt(i);
                    //yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    public IEnumerator Attack(Unit chosenUnit, Unit Target)
    {
        StatusText.text = "Attacking!";
        chosenUnit.PerformAttack();
        float result = RandomC() + GodOfFortune * (sanity.perc);
        if (result <= 0.35/chosenUnit.hitMod) //Miss
        {
            yield return StartCoroutine(RandomNumberVis("Missed!"));
            PopUpTextController.CreatePopUpText("MISSED", enemiesUI[Target.index].transform);
            yield return new WaitForSeconds(1.0f);
            dice.text.text = "...";
            GodOfFortune += 0.1f;
        } 
        else if(result >= 0.70*chosenUnit.critMod) //Crit
        {
            dice.gameObject.GetComponent<TMPro.Examples.VertexJitter>().AngleMultiplier = 20;
            dice.gameObject.GetComponent<TMPro.Examples.VertexJitter>().CurveScale = 200;
            yield return StartCoroutine(RandomNumberVis("CRIT!"));
            PopUpTextController.CreatePopUpText("CRIT!", enemiesUI[Target.index].transform);
            float damage = Mathf.Round(2 * chosenUnit.damageMod * 1.25f * UnityEngine.Random.Range(0.5f, 1.5f) * 100f) / 100f;
            Target.TakeDamage2(damage);
            yield return StartCoroutine(DamageUnit(Target.index, damage));
            dice.gameObject.GetComponent<TMPro.Examples.VertexJitter>().AngleMultiplier = 3;
            dice.gameObject.GetComponent<TMPro.Examples.VertexJitter>().CurveScale = 5;
            dice.text.text = "...";
            GodOfFortune = 0.0f;
        }
        else
        {
            yield return StartCoroutine(RandomNumberVis("HIT!"));
            float damage = Mathf.Round(2 * chosenUnit.damageMod * UnityEngine.Random.Range(0.5f, 1.5f) * 100f) / 100f;
            Target.TakeDamage2(damage);
            yield return StartCoroutine(DamageUnit(Target.index, damage));
            dice.text.text = "...";
            GodOfFortune = 0.0f;
        }
    }

    public IEnumerator Defend(Unit chosenUnit, Unit Target)
    {
        StatusText.text = "Defending!";
        chosenUnit.Defend();
        Target.defended = true;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Move(Unit chosenUnit, Move chosenMove, Unit Target)
    {
        StatusText.text = "Perform Moves!";
        //Debug.Log("Move!!");
        chosenUnit.MakeMove(chosenMove);
        if (RandomSystem.RandomValue() + GodOfFortune <= 0.75f)
        {
            yield return StartCoroutine(RandomNumberVis("HIT BY MOVE!"));
            if (chosenMove.Damage > 0)
            {
                yield return StartCoroutine(DamageFriendlyUnit(Target.attachedObject, chosenMove.Damage));
                Target.TakeDamage2(chosenMove.Damage);
            }
            StartCoroutine(Target.HitByMove(chosenMove));

            foreach (Effect effect in chosenMove.Effects)
            {
                yield return StartCoroutine(ApplyStatusEffect(Target, effect));
            }
            yield return new WaitForSeconds(1.0f);
            dice.text.text = "...";
            GodOfFortune = 0.0f; 
        }
        else
        {
            yield return StartCoroutine(RandomNumberVis("MISSED MOVE!"));
            yield return new WaitForSeconds(1.0f);
            dice.text.text = "...";
            GodOfFortune += 0.1f;
        }
    }

    public IEnumerator DamageUnit(int index, float dam)
    {
        Debug.Log(index);
        PopUpTextController.CreatePopUpText(dam.ToString(),enemiesUI[index].transform);
        yield return StartCoroutine(enemiesUI[index].Shake());
    }

    public IEnumerator DamageFriendlyUnit(GameObject element, float dam)
    {
        Debug.Log(element.name);
        PopUpTextController.CreatePopUpText(dam.ToString(), element.transform);
        yield return StartCoroutine(element.GetComponent<EnemyButton>().Shake());
    }

    public IEnumerator PEffect(GameObject element, string what, Color color)
    {
        //Debug.Log(element.name);
        PopUpTextController.CreatePopUpText(what, element.transform);
        yield return StartCoroutine(element.GetComponent<EnemyButton>().Particles(color));
    }

    public IEnumerator RandomNumberVis(string s)
    {
        yield return StartCoroutine(dice.RandomRoll(s));
    }

    public bool RandomChance(float chance)
    {
        return UnityEngine.Random.value <= chance;
    }

    public float RandomC()
    {
        return RandomSystem.RandomGaussian();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
