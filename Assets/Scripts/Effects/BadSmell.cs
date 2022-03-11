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
    public void OnInflict()
    {
        Debug.Log("Giving the unit a bad smell: " + affected.Name);
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
        Debug.Log("Bad smell gone: " + affected.Name);
    }

    void Start()
    {
        AppliedState = UnitStates.ASLEEP;
        Modifiers.Add("Stunned for", 2);
        //EffectName = "Sleeping";
        //Description = "The unit is sleeping.";
    }
}
