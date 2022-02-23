using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect: MonoBehaviour, IEnumerable
{
    public UnitStates appliedState;
    public Dictionary<string, float> modifiers;

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)modifiers).GetEnumerator();
    }
}
