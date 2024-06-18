using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCurveData", menuName = "Data/PlayerCurveData")]
public class PlayerCurveData : ScriptableObject
{
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallForceCurve;

    public AnimationCurve MoveCurve => _moveCurve;
    public AnimationCurve JumpCurve => _jumpCurve;
    public AnimationCurve FallForceCurve => _fallForceCurve;
}
