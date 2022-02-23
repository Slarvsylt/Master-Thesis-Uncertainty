using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStates {DAZED, STUNNED, ASLEEP, BURNING, PAIN, SAD, HAPPY, POISONED, 
                        PARALYZED, SLOWED, CRYING, DRUNK, FULL, REFRESHED, ENCOURAGED,
                        AFRAID, INSANE, CHARMED, HORNY, HUNGRY, THIRSTY }
public interface Unit
{
    void PerformAttack(Unit Target);
    void Defend(Unit Target);
    void TakeDamage(float damage);
    void RestoreHP(float heal);
    void MakeMove(Move move);
    void Die();
    void HitByMove(Move move);
}
