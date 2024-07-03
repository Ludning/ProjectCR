using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Player Owner;
    [SerializeField] private Transform LeftHand;
    [SerializeField] private Transform RightHand;
    
    [SerializeField] Weapon PrimaryWeapon;
    [SerializeField] Weapon SubWeapon;

    private WeaponIndexType _currentWeaponIndex;

    private void FixedUpdate()
    {
        
    }

    //무기를 1, 2번 키로 스왑시
    public void SwapWeapon(WeaponIndexType index)
    {
        if (GetHoldWeapon(index) == null)
            return;
        if (_currentWeaponIndex == index)
            return;
        _currentWeaponIndex = index;
        SetActiveCurrentWeapon();
    }

    //현재 무기 반환
    public Weapon GetHoldWeapon(WeaponIndexType index)
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
    //비소지 무기 반환
    public Weapon GetFreeWeapon(WeaponIndexType index)
    {
        switch (index)
        {
            case WeaponIndexType.Primary:
                return SubWeapon;
            case WeaponIndexType.Secondary:
                return PrimaryWeapon;
            default:
                return null;
        }
    }
    
    //무기를 인벤토리등에서 변경시
    public void EquipmentIndexWeapon(WeaponIndexType index, Transform weaponTransform)
    {
        switch (index)
        {
            case WeaponIndexType.Primary:
                weaponTransform.SetParent(RightHand);
                PrimaryWeapon = weaponTransform.GetComponent<Weapon>();
                PrimaryWeapon.InitWeapon(Owner, this);
                break;
            case WeaponIndexType.Secondary:
                weaponTransform.SetParent(RightHand);
                SubWeapon = weaponTransform.GetComponent<Weapon>();
                SubWeapon.InitWeapon(Owner, this);
                break;
        }
        SetActiveCurrentWeapon();
    }
    
    //현재 무기 활성화, 나머지 무기 비활성화
    private void SetActiveCurrentWeapon()
    {
        Weapon holdWeapon = GetHoldWeapon(_currentWeaponIndex);
        holdWeapon.ReceptionWeaponHandlerEvent(WeaponTrigger.HoldWeapon);
        holdWeapon.gameObject.SetActive(true);
        
        Weapon freeWeapon = GetFreeWeapon(_currentWeaponIndex);
        freeWeapon.ReceptionWeaponHandlerEvent(WeaponTrigger.FreeWeapon);
        freeWeapon.gameObject.SetActive(false);
    }

    #region Editor Function
    void OnValidate()
    {
        CacheHandTransforms();
    }

    private void CacheHandTransforms()
    {
        LeftHand = FindBoneTransform("Bip001 L Hand_WeaponBone");
        RightHand = FindBoneTransform("Bip001 R Hand_WeaponBone");

        if (LeftHand != null && RightHand != null)
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
