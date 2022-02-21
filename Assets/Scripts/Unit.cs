using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Unit
{
    float HP { get; set; }
    float MP { get; set; }
    void PerformAttack();
    void Defend();
    List<Move> Moves { get; set; }
}
