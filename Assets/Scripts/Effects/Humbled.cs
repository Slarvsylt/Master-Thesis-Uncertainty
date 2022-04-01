using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humbled : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Humbled";
    public string Description { get; set; } = "The unit is humbled!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        affected.damageMod -= 0.2f;
        affected.hitMod -= 0.3f;
        yield return StartCoroutine(affected.Effect("I' humbled! *Humble*", Color.yellow));

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
        affected.damageMod += 0.2f;
        affected.hitMod += 0.3f;
        yield return StartCoroutine(affected.Effect("I'm no longer humbled! *confident*", Color.yellow));
    }
}
