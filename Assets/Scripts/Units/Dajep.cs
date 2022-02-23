using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dajep : MonoBehaviour, Unit
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

    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        Strengths.Add(MoveType.MENTAL);
        Strengths.Add(MoveType.STRONG);
        Weaknesses.Add(MoveType.COOKING);
        AttackTypes.Add(MoveType.BIG);
    }

    void AddMoves()
    {
        Moves.Add(new Sleep(1, true, new List<MoveType>() { MoveType.MAGIC }, new List<Effect>() {new Sleeping(2)} ));
    }

    void CheckAndApplyCurrentEffects()
    {

    }

    public void PerformAttack(Unit Target)
    {
        
    }
    public void Defend(Unit Target)
    {

    }

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

    public void MakeMove(Move move)
    {
        currentMP -= move.MPcost;
    }

    public void HitByMove(Move move)
    {
        CurrentEffects.AddRange(move.effects);
    }

    public void Die()
    {

    }

}
