using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Burning";
    public string Description { get; set; } = "The unit is burning! Fuck!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        //Debug.Log("owie I'm on fire!" + affected.Name);
        yield return StartCoroutine(affected.TakeDamage1(RandomSystem.RandomRange(0.5f, 2)));
        yield return StartCoroutine(affected.Effect("Fire, OWIE!", Color.red));
        //Stun Unit
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        yield return StartCoroutine(affected.Effect("I'm no longer burning!", Color.red));
    }
}
