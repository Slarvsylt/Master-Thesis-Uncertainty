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

    public GameObject attachedObject;

    [SerializeField]
    public List<Move> Moves;
    public List<Effect> CurrentEffects = new List<Effect>();
    public List<Effect> CurrentEffectsPerm;
    [SerializeField]
    public List<MoveType> Strengths;
    public List<MoveType> Weaknesses;
    public List<MoveType> AttackTypes;

    public bool defended;
    [SerializeField]
    public bool stunned = false;
    [SerializeField]
    public bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        isDead = false;
        //Debug.Log("New Unit " + Name);
    }
    public void PerformAttack() 
    {
        //Play animation or something
    }
    public void Defend() 
    {
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage");
        StartCoroutine(TakeDamage1(damage));
    }

    public IEnumerator TakeDamage1(float damage)
    {
        Debug.Log("TakeDamage1");
        currentHP -= damage / defMod;
        yield return StartCoroutine(GameSystem.gameSystem.DamageFriendlyUnit(attachedObject, damage));
        if (currentHP <= 0)
            StartCoroutine(Die());
    }

    public IEnumerator Effect(string what, Color color)
    {
        yield return StartCoroutine(GameSystem.gameSystem.PEffect(attachedObject, what, color));
    }

    public void TakeDamage2(float damage)
    {
        Debug.Log("TakeDamage2");
        currentHP -= damage / defMod;
        if (currentHP <= 0)
            StartCoroutine(Die());
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
        //Make move.
        //Animation 
    }

    /// <summary>
    /// Tells the unit to die.
    /// </summary>
    public IEnumerator Die() 
    {
        isDead = true;
        Debug.Log(Name + " died!");
        yield break;
        //Inactivate unit and display corpse
    }

    public void Ressurect()
    {
        isDead = false;
        currentHP = 1.0f;
        Debug.Log(Name + " is alive once again!");
    }
    
    public IEnumerator HitByMove(Move move) 
    {
        yield break;
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
