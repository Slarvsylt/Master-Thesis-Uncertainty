using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sad : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Sad";
    public string Description { get; set; } = "The unit is sad! Fuck!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 8;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        //Debug.Log("owie I'm on fire!" + affected.Name);
        yield return StartCoroutine(affected.Effect("I'm not feeling very happy right now...", Color.black));
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
