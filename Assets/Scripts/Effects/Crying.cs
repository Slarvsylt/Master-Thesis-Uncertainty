using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crying : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Crying";
    public string Description { get; set; } = "The unit is crying!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 8;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        affected.damageMod -= 0.2f;
        affected.hitMod += 0.3f;
        yield return StartCoroutine(affected.Effect("Boho, I'm sad!", Color.blue));

    }
    public IEnumerator OnTurnBegin()
    {
        yield return StartCoroutine(affected.Effect("Boho! Bohoho!", Color.blue));
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        affected.damageMod += 0.2f;
        affected.hitMod -= 0.3f;
        yield return StartCoroutine(affected.Effect("I'm no longer sad!", Color.yellow));
    }
}
