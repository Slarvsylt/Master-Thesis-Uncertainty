using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Effect
{
    string EffectName { get; set; }
    void OnInflict();
    void OnTurnBegin();
    void OnTurnEnd();
    void OnRemoved(); 
}
