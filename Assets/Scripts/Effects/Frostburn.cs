using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostburn : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Frostburn";
    public string Description { get; set; } = "Painfully cold...";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        yield return StartCoroutine(affected.TakeDamage1(RandomSystem.RandomRange(0.8f, 1.6)));
        yield return StartCoroutine(affected.Effect("I can't feel my fingers!", Color.cyan));

    }
    public IEnumerator OnRemoved()
    {
        //Debug.Log("Putting the fire out: " + affected.Name);
        yield break;
    }
}
