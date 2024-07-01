using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    #region Editor Function
    void OnValidate()
    {
        CacheHandTransforms();
    }

    private void CacheHandTransforms()
    {
        _leftHand = FindBoneTransform("Bip001 L Hand_WeaponBone");
        _rightHand = FindBoneTransform("Bip001 R Hand_WeaponBone");

        if (_leftHand != null && _rightHand != null)
        {
            Debug.Log("왼손과 오른손 본이 성공적으로 캐싱되었습니다.");
        }
        else
        {
            Debug.LogWarning("왼손 또는 오른손 본을 찾지 못했습니다.");
        }
    }

    private Transform FindBoneTransform(string boneName)
    {
        Transform[] allTransforms = GetComponentsInChildren<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.name == boneName)
            {
                return t;
            }
        }
        return null;
    }
    #endregion
}
