using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dajep : MonoBehaviour, Unit
{

    public float maxHP { get; set; }
    public float maxMP { get; set; }
    public float hitMod { get; set; }
    public float critMod { get; set; }
    public float damageMod { get; set; }
    public float defMod { get; set; }
    public List<Move> Moves { get; set; }
    public List<UnitStates> CurrentEffects { get; set; }

    void Start()
    {

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

    }
    public void RestoreHP(float heal)
    {

    }

    public void MakeMove(Move move)
    {

    }

}
