using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chilly : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Chilly";
    public string Description { get; set; } = "It is Chilly.";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 10;
    public IEnumerator OnInflict()
    {
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        yield break;
    }
}
