using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Poisoned";
    public string Description { get; set; } = "Poisoned!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 9;
    public IEnumerator OnInflict()
    {
        yield return StartCoroutine(affected.Effect("POISONed!", Color.green));
        //Debug.Log("Putting the unit on fire: " + affected.Name);
    }
    public IEnumerator OnTurnBegin()
    {
        //Debug.Log("owie I'm on fire!" + affected.Name);
        yield return StartCoroutine(affected.TakeDamage1(1));

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
