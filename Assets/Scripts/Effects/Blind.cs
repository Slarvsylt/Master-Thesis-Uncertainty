using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Blind";
    public string Description { get; set; } = "The unit is blind! Totally blind!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 50;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        affected.hitMod += 1;
        yield return StartCoroutine(affected.Effect("I'm now blind and cannot see ever again", Color.black));
    }
    public IEnumerator OnTurnBegin()
    {
        //Debug.Log("owie I'm on fire!" + affected.Name);
        yield return StartCoroutine(affected.Effect("Unfortunately, I'm still blind... ", Color.black));

        //Stun Unit
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        //Debug.Log("Putting the fire out: " + affected.Name);
        affected.hitMod -= 1;
        yield return StartCoroutine(affected.Effect("It's a miracle! I'm not blind anymore!", Color.white));
    }
}
