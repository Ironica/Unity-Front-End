using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum Block
{
  [EnumMember(Value = "OPEN")]
  OPEN,

  [EnumMember(Value = "BLOCKED")]
  BLOCKED,

  [EnumMember(Value = "LOCK")]
  LOCK,

  [EnumMember(Value = "STAIR")]
  STAIR,

  [EnumMember(Value = "VOID")]
  VOID,
}
