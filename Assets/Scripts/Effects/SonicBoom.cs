using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBoom : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "SonicBoom";
    public string Description { get; set; } = "The unit has had their eardrums blown out. \"Huh? What was that?\" ";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 3;
    public IEnumerator OnInflict()
    {
        affected.TakeDamage(2);
        Debug.Log("Destroying the hearing of unit: " + affected.Name);
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
        Debug.Log("Eardrums has healed: " + affected.Name);
        yield break;
    }

}
