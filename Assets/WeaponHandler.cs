using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    
    [SerializeField] Transform PrimaryWeapon;
    [SerializeField] Transform SubWeapon;

    private WeaponIndexType _currentWeaponIndex;
    
    //무기를 1, 2번 키로 스왑시
    public void SwapWeapon(WeaponIndexType index)
    {
        if (GetIndexWeapon(index) == null)
            return;
        if (_currentWeaponIndex == index)
            return;
        _currentWeaponIndex = index;
        SetActiveCurrentWeapon();
    }

    public Transform GetIndexWeapon(WeaponIndexType index)
    {
        switch (index)
        {
            case WeaponIndexType.Primary:
                return PrimaryWeapon;
            case WeaponIndexType.Secondary:
                return SubWeapon;
            default:
                return null;
        }
    }
    //무기를 인벤토리등에서 변경시
    public void SetIndexWeapon(WeaponIndexType index, Transform weaponTransform)
    {
        switch (index)
        {
            case WeaponIndexType.Primary:
                weaponTransform.SetParent(_rightHand);
                PrimaryWeapon = weaponTransform;
                break;
            case WeaponIndexType.Secondary:
                weaponTransform.SetParent(_rightHand);
                SubWeapon = weaponTransform;
                break;
        }
        SetActiveCurrentWeapon();
    }
    
    private void SetActiveCurrentWeapon()
    {
        if (_currentWeaponIndex == WeaponIndexType.Primary)
        {
            PrimaryWeapon.gameObject.SetActive(true);
            SubWeapon.gameObject.SetActive(false);
        }
        else
        {
            PrimaryWeapon.gameObject.SetActive(false);
            SubWeapon.gameObject.SetActive(true);
        }
    }

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
