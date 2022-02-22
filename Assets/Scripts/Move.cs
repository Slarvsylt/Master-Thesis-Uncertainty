using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Move
{
    bool requireTarget { get; set; }
    float MPcost { get; set; }
}
