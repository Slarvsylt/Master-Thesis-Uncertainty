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
    public void OnInflict()
    {
        Debug.Log("Giving the unit a headache: " + affected.Name);
    }
    public void OnTurnBegin()
    {
    }
    public void OnTurnEnd()
    {
        Debug.Log("owie my head!");
        affected.TakeDamage(1);
    }
    public void OnRemoved()
    {
        Debug.Log("Headache pills: " + affected.Name);
    }

    void Start()
    {
        AppliedState = UnitStates.ASLEEP;
        //EffectName = "Sleeping";
        //Description = "The unit is sleeping.";
    }
}
