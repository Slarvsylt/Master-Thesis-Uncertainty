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
    public void OnInflict()
    {

    }
    public void OnTurnBegin()
    {
        //Stun Unit
    }
    public void OnTurnEnd()
    {

    }
    public void OnRemoved()
    {
        
    }

    void Start()
    {
        AppliedState = UnitStates.ASLEEP;
        Modifiers.Add("Stunned for", 2);
        //EffectName = "Sleeping2";
        Description = "The unit is sleeping.";
    }
}