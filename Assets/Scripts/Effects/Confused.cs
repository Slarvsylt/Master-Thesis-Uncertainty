using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confused : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Confused";
    public string Description { get; set; } = "The unit is confused! What?!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 5;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        affected.hitMod += 0.3f;
        yield return StartCoroutine(affected.Effect("I'm Confused! What?", Color.blue));

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
        yield return StartCoroutine(affected.Effect("I'm no longer Confused!", Color.blue));
        affected.hitMod -= 0.3f;
        yield break;
    }
}
