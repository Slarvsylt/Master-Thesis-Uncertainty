using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStates {DAZED, STUNNED, ASLEEP, BURNING, PAIN, SAD, HAPPY, POISONED,PARALYZED, 
    SLOWED, CRYING, DRUNK, FULL, REFRESHED, ENCOURAGED, AFRAID, INSANE, CHARMED, HORNY, HUNGRY, THIRSTY }
public class Unit: MonoBehaviour
{

    public float currentHP;
    public float currentMP;
    public int index;

    [SerializeField]
    public string Name;
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

    public bool defended;
    public bool stunned = false;
    public bool isDead = false;

    public void PerformAttack() 
    {
        //Play animation or something
    }
    public void Defend() 
    {
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage*defMod;
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
        //Make move.
        //Animation 
    }

    /// <summary>
    /// Tells the unit to die.
    /// </summary>
    public void Die() 
    {
        isDead = true;
        Debug.Log(Name + " died!");
        //Inactivate unit and display corpse
    }
    public void HitByMove(Move move) 
    {
        //Hit by move
        //Apply status effects
    }
    public void AddMoves(List<Move> moves)
    {
        Moves = moves;
    }

    public void CheckAndApplyCurrentEffects()
    {
        //Maybe should be done elsewhere
    }
}
