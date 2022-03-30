using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSmell : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "BadSmell";
    public string Description { get; set; } = "Something stinks in here...";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        Debug.Log("Giving the unit a bad smell: " + affected.Name);
        yield return StartCoroutine(affected.Effect("Ew!", Color.green));
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        //Stun Unit
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        Debug.Log("Bad smell gone: " + affected.Name);
        yield break;
    }
}
