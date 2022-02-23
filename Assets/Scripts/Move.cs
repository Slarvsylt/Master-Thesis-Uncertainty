using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    MAGIC, SNEAKY, BLUNT, SHARP, PAIN, FIRE, FROST, POISON, MENTAL, DIVINE, COOKING, ELECTRIC, WIND, STRONG, WEAK, BIG, SMALL, FAST, SLOW, GUN
}

public class Move: MonoBehaviour
{
    public bool requireTarget { get; set; }
    public float MPcost { get; set; }
    public List<MoveType> MoveTypes { get; set; }
    public List<Effect> effects { get; set; }
}
