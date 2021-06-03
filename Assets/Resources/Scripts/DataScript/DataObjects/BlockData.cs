using System.Runtime.Serialization;

namespace Resources.Scripts.DataScript.DataObjects
{
    
    // This is the enum that will be send to the server
    public enum BlockData
    {
        [EnumMember(Value = "OPEN")]
        OPEN,
        [EnumMember(Value = "BLOCKED")]
        BLOCKED,
        [EnumMember(Value = "STAIR")]
        STAIR,
        [EnumMember(Value = "LOCK")]
        LOCK,
        [EnumMember(Value = "VOID")]
        VOID,
    }
}