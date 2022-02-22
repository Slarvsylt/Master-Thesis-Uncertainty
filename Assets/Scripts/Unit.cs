using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStates {DAZED, STUNNED, ASLEEP, BURNING, PAIN, SAD, HAPPY, POISONED, PARALYZED, SLOWED, CRYING, DRUNK, FULL, REFRESHED, ENCOURAGED, AFRAID, INSANE, CHARMED, HORNY, HUNGRY, THIRSTY }
public interface Unit
{

    float maxHP { get; set; }
    float maxMP { get; set; }
    float hitMod { get; set; }
    float critMod { get; set; }
    float damageMod { get; set; }
    float defMod { get; set; }
    void PerformAttack(Unit Target);
    void Defend(Unit Target);
    void TakeDamage(float damage);
    void RestoreHP(float heal);
    void MakeMove(Move move);
    List<UnitStates> CurrentEffects { get; set; }
    List<Move> Moves { get; set; }
}
