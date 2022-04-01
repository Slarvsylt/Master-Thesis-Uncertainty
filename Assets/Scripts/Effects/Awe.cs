using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awe : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Awe";
    public string Description { get; set; } = "The unit is Awestruck! Wow!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 6;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        affected.hitMod += 0.2f;
        yield return StartCoroutine(affected.Effect("OOhh, AAhh!", Color.yellow));

    }
    public IEnumerator OnTurnBegin()
    {
        yield return StartCoroutine(affected.Effect("OOhh, AAhh!", Color.yellow));
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        yield return StartCoroutine(affected.Effect("I'm no longer awed!", Color.yellow));
        affected.hitMod -= 0.2f;
    }
}
