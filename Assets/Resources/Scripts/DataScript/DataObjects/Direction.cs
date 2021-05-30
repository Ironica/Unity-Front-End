using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum Direction
{
    
    [EnumMember(Value = "UP")]
    UP,
    
    [EnumMember(Value = "DOWN")]
    DOWN,
    
    [EnumMember(Value = "LEFT")]
    LEFT,
    
    [EnumMember(Value = "RIGHT")]
    RIGHT
}
