using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dajep : Unit
{

    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        Strengths.Add(MoveType.MENTAL);
        Strengths.Add(MoveType.STRONG);
        Weaknesses.Add(MoveType.COOKING);
        AttackTypes.Add(MoveType.BIG);
    }
}
