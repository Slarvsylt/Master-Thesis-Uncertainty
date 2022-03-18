using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Effect
{
    string EffectName { get; set; }
    [SerializeField]
    public Unit affected { get; set; }
    public int maxTurns { get; set; }
    IEnumerator OnInflict();
    IEnumerator OnTurnBegin();
    IEnumerator OnTurnEnd();
    IEnumerator OnRemoved(); 
}
