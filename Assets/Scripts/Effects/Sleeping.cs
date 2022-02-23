using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : Effect
{

    public Sleeping(int duration)
    {
        appliedState = UnitStates.ASLEEP;
        modifiers.Add("Stunned for", duration);
    }
}
