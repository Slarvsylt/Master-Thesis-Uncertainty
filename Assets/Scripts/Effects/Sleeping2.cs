using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping2 : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Sleeping2";
    public string Description { get; set; }
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
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
        yield break;
    }
}
