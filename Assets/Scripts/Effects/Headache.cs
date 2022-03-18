using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headache : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Headache";
    public string Description { get; set; } = "Ow, my head!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        Debug.Log("Giving the unit a headache: " + affected.Name);
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        Debug.Log("owie my head!");
        affected.TakeDamage(1);
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        Debug.Log("Headache pills: " + affected.Name);
        yield break;
    }
}
