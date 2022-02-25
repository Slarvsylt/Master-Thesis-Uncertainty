using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectMoment { START, END }

public interface Effect
{
    string EffectName { get; set; }
    EffectMoment effectMoment { get; set; }
    void OnInflict();
    void OnTurnBegin();
    void OnTurnEnd();
    void OnRemoved(); 
}
