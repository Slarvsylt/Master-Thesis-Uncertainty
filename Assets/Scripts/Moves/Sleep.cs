using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : Move
{
    public void Effect(Unit Target)
    {
        Target.HitByMove(this);
    }

    public Sleep(float MPcost, bool requireTarget, List<MoveType> MoveTypes, List<Effect> effects)
    {
        this.MPcost = MPcost;
        this.requireTarget = requireTarget;
        this.MoveTypes = MoveTypes;
        this.effects = effects;
    }
}
