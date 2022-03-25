using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pain : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Pain";
    public string Description { get; set; } = "Severe pain!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 6;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        yield return StartCoroutine(affected.Effect("Pain, OWIE!", Color.red));
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
        //Debug.Log("Putting the fire out: " + affected.Name);
        yield break;
    }
}
