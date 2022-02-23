using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStates {DAZED, STUNNED, ASLEEP, BURNING, PAIN, SAD, HAPPY, POISONED,PARALYZED, 
    SLOWED, CRYING, DRUNK, FULL, REFRESHED, ENCOURAGED, AFRAID, INSANE, CHARMED, HORNY, HUNGRY, THIRSTY }
public class Unit: MonoBehaviour
{

    public float currentHP;
    public float currentMP;

    [SerializeField]
    public float maxHP;
    [SerializeField]
    public float maxMP;
    [SerializeField]
    public float hitMod;
    [SerializeField]
    public float critMod;
    [SerializeField]
    public float damageMod;
    [SerializeField]
    public float defMod;

    public List<Move> Moves;
    public List<Effect> CurrentEffects;
    public List<Effect> CurrentEffectsPerm;
    public List<MoveType> Strengths;
    public List<MoveType> Weaknesses;
    public List<MoveType> AttackTypes;

    public void PerformAttack(Unit Target) { }
    public void Defend(Unit Target) { }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Die();
    }
    public void RestoreHP(float heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
    public void MakeMove(Move move) { }
    public void Die() { }
    public void HitByMove(Move move) { }
    public void AddMoves()
    {

    }

    public void CheckAndApplyCurrentEffects()
    {

    }
}
