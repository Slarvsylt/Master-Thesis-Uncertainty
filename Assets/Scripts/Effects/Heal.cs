using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Heal";
    public string Description { get; set; } = "Heal unit!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 8;
    public IEnumerator OnInflict()
    {
        if (affected.isDead)
        {
            yield return StartCoroutine(affected.Ressurect());
        }
        else
        {
            yield return StartCoroutine(affected.RestoreHP(RandomSystem.RandomRange(1,4)));
        }
    }
    public IEnumerator OnTurnBegin()
    {
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        yield break;
    }
}
