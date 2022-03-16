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
    public void OnInflict()
    {
        affected.stunned = true;
        Debug.Log("Putting the unit to sleep: " + affected.Name);
    }
    public void OnTurnBegin()
    {
        affected.stunned = true;
        //Stun Unit
    }
    public void OnTurnEnd()
    {

    }
    public void OnRemoved()
    {
        affected.stunned = false;
        Debug.Log("Awaking: " + affected.Name);
    }

    void Start()
    {
        AppliedState = UnitStates.ASLEEP;
        Modifiers.Add("Stunned for", 2);
        //EffectName = "Sleeping";
        //Description = "The unit is sleeping.";
    }
}
