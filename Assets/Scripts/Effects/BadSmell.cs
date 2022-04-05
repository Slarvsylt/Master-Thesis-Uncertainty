using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSmell : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Bad Smell";
    public string Description { get; set; } = "Something stinks in here...";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        yield return StartCoroutine(affected.Effect("Ew! What's that smell?", Color.green));
    }
    public IEnumerator OnTurnBegin()
    {
        //Stun Unit
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        yield return StartCoroutine(affected.Effect("It still smells bad in here!", Color.green));
    }
    public IEnumerator OnRemoved()
    {
        yield return StartCoroutine(affected.Effect("I think the smell is gone...", Color.green));
    }
}
