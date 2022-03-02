using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ooma : Unit
{
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        List<Move> NewMoves = new List<Move> { MoveDatabase.Instance.GetMove("sleep"), MoveDatabase.Instance.GetMove("sleep2"), MoveDatabase.Instance.GetMove("sleep3") };
        AddMoves(NewMoves);
    }
}
