using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Sleeping";
    public string Description { get; set; } = "Schh... the Unit is sleeping... ";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        affected.stunned = true;
        Debug.Log("Putting the unit to sleep: " + affected.Name);
        yield break;
    }
    public IEnumerator OnTurnBegin()
    {
        affected.stunned = true;
        //Stun Unit
        yield break;
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        affected.stunned = false;
        Debug.Log("Awaking: " + affected.Name);
        yield break;
    }
}
