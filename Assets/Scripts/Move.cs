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
    public List<string> Effects { get; set; }
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
        this.Effects = Effects;
        this.ObjectSlug = ObjectSlug;
        this.Description = Description;
        this.MoveName = MoveName;
        this.Damage = Damage;
    }
}
