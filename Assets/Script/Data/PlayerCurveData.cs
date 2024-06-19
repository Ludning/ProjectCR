using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCurveData", menuName = "Data/PlayerCurveData")]
public class PlayerCurveData : ScriptableObject
{
    [SerializeField] private AnimationCurve _moveCurve;

    public AnimationCurve MoveCurve => _moveCurve;
}
