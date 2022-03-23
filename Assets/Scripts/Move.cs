using System.Collections;
using System;   
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum MoveType
{
    MAGIC, SNEAKY, BLUNT, SHARP, PAIN, FIRE, FROST, POISON, MENTAL, DIVINE, COOKING, ELECTRIC, WIND, STRONG, WEAK, BIG, SMALL, FAST, SLOW, GUN
}

public class Move
{
    public bool RequireTarget { get; set; }
    public float MPcost { get; set; }
    public MoveType MoveType { get; set; }
    public List<Effect> Effects { get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    public string MoveName { get; set; }
    public float Damage { get; set; }


    [JsonConstructor]
    public Move(bool RequireTarget, float MPcost, int MoveType, List<string> Effects, string ObjectSlug, string Description, string MoveName, float Damage)
    {
        this.RequireTarget = RequireTarget;
        this.MPcost = MPcost;
        //this.MoveType = (MoveType)Enum.Parse(typeof(MoveType), MoveType);
        //this.MoveType = MoveType;
        this.MoveType = (MoveType)MoveType;
        this.Effects = new List<Effect>();
        foreach (string e in Effects)
        {
            //Debug.Log("Effects/" + e);
            GameObject go = GameObject.Instantiate((GameObject)Resources.Load("Effects/" + e), GameObject.Find("Canvas").transform);
            Effect ef = go.GetComponent<Effect>();
            this.Effects.Add(ef);
        }
        this.ObjectSlug = ObjectSlug;
        this.Description = Description;
        this.MoveName = MoveName;
        this.Damage = Damage;
    }

    public Move(Move move)
    {
        this.RequireTarget = move.RequireTarget;
        this.MPcost = move.MPcost;
        //this.MoveType = (MoveType)Enum.Parse(typeof(MoveType), MoveType);
        //this.MoveType = MoveType;
        this.MoveType = (MoveType)move.MoveType;
        this.Effects = new List<Effect>();
        foreach (Effect e in move.Effects)
        {
            //Debug.Log("Effects/" + e.EffectName);
            GameObject go = GameObject.Instantiate((GameObject)Resources.Load("Effects/" + e.EffectName), GameObject.Find("Canvas").transform);
            Effect ef = go.GetComponent<Effect>();
            this.Effects.Add(ef);
        }
        this.ObjectSlug = move.ObjectSlug;
        this.Description = move.Description;
        this.MoveName = move.MoveName;
        this.Damage = move.Damage;
    }
}
