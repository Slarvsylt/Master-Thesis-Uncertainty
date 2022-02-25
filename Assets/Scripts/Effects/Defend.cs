using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour, Effect
{
    public EffectMoment effectMoment { get; set; }
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; }
    public string Description { get; set; }
    public Unit affected { get; set; }
    public void OnInflict()
    {

    }
    public void OnTurnBegin()
    {

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
        EffectName = "Sleeping";
        Description = "The unit is sleeping.";
    }
}
