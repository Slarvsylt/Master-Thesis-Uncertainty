using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBoom : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Sonic Boom";
    public string Description { get; set; } = "The unit has had their eardrums blown out. \"Huh? What was that?\" ";
    public Unit affected { get; set; }
    public void OnInflict()
    {
        affected.TakeDamage(2);
        Debug.Log("Destroying the hearing of unit: " + affected.Name);
    }
    public void OnTurnBegin()
    {
    }
    public void OnTurnEnd()
    {

    }
    public void OnRemoved()
    {
        Debug.Log("Eardrums has healed: " + affected.Name);
    }

    void Start()
    {
        //EffectName = "Sleeping";
        //Description = "The unit is sleeping.";
    }
}
