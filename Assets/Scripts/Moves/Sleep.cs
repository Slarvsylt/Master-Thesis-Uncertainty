using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour, Move
{
    public bool requireTarget { get; set; }
    public float MPcost { get; set; }

    public void Effect(Unit Target)
    {
        Target.CurrentEffects.Add(UnitStates.ASLEEP);
    }
}
