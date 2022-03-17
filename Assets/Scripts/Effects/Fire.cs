using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Fire";
    public string Description { get; set; } = "The unit is burning! Fuck!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public void OnInflict()
    {
        Debug.Log("Putting the unit on fire: " + affected.Name);
    }
    public void OnTurnBegin()
    {
        Debug.Log("owie I'm on fire!" + affected.Name);
        affected.TakeDamage(1);
        //Stun Unit
    }
    public void OnTurnEnd()
    {

    }
    public void OnRemoved()
    {
        Debug.Log("Putting the fire out: " + affected.Name);
    }

    void Start()
    {
        AppliedState = UnitStates.ASLEEP;
        Modifiers.Add("Stunned for", 2);
        //EffectName = "Sleeping";
        //Description = "The unit is sleeping.";
    }
}
