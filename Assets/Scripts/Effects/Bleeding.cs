using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour, Effect
{
    public UnitStates AppliedState { get; set; }
    public Dictionary<string, int> Modifiers { get; set; }
    public string EffectName { get; set; } = "Bleeding";
    public string Description { get; set; } = "The unit is bleeding profusely! Not good!";
    public Unit affected { get; set; }
    public int maxTurns { get; set; } = 4;
    public IEnumerator OnInflict()
    {
        //Debug.Log("Putting the unit on fire: " + affected.Name);
        yield return StartCoroutine(affected.Effect("AAUUUGGH, I'm bleeding profusely from my abdomen!", Color.red));

    }
    public IEnumerator OnTurnBegin()
    {
        yield return StartCoroutine(affected.TakeDamage1(RandomSystem.RandomRange(0.5f,2)));
    }
    public IEnumerator OnTurnEnd()
    {
        yield break;
    }
    public IEnumerator OnRemoved()
    {
        yield return StartCoroutine(affected.Effect("I managed to stop the bleeding!", Color.red));
    }
}
