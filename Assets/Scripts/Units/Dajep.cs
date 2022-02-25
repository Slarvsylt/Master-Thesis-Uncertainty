using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dajep : Unit
{
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        List<Move> NewMoves = new List<Move> { MoveDatabase.Instance.GetMove("Sleep"), MoveDatabase.Instance.GetMove("Sleep"), MoveDatabase.Instance.GetMove("Sleep") };
        AddMoves(NewMoves);
    }
}
