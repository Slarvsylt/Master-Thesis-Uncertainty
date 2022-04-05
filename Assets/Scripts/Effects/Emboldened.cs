using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emboldened : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Emboldened";
    public string Description { get; set; } = "Hurray! Nothing can stop me!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 6;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        affected.damageMod += 1;
        yield return StartCoroutine(affected.Effect("Haha! Come and get it!", Color.yellow));
    }
    public IEnumerator OnTurnBegin()
    {
        //Debug.Log("owie I'm on fire!" + affected.Name);
        yield return StartCoroutine(affected.Effect("Haha! Come and get it!", Color.yellow));
        //Stun Unit
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        affected.damageMod -= 1;
        //Debug.Log("Putting the fire out: " + affected.Name);
        yield break;
    }
}
