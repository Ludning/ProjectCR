using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControlData", menuName = "Data/PlayerControlData")]
public class PlayerControlData : ScriptableObject
{
    public float MoveSpeed;
    public float RotateSpeed;
    public float RunMultiply;
    public float JumpForce;
    public float GravityValue;
    public float CurveMaxTime;
}
