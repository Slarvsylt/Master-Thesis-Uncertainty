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
    public int maxTurns { get; set; } = 8;
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
        yield return StartCoroutine(affected.TakeDamage1(RandomSystem.RandomRange(0.1f, 1)));
        yield return StartCoroutine(affected.Effect("My head, OWIE!", Color.red));
    }
    public IEnumerator OnRemoved()
    {
        Debug.Log("Headache pills: " + affected.Name);
        yield break;
    }
}
