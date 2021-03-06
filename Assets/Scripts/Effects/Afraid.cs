using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afraid : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Afraid";
    public string Description { get; set; } = "The unit is afraid! Fuck!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 4;
    public IEnumerator OnInflict()
    {
        affected.critMod -= 0.2f;
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        yield return StartCoroutine(affected.Effect("Boho, I'm afraid!", Color.blue));
    }
    public IEnumerator OnTurnBegin()
    {
        //Debug.Log("owie I'm on fire!" + affected.Name);
        yield break;
        //Stun Unit
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        affected.critMod += 0.2f;
        yield return StartCoroutine(affected.Effect("Hoho, I'm no longer afraid!", Color.blue));
        //Debug.Log("Putting the fire out: " + affected.Name);
    }
}
