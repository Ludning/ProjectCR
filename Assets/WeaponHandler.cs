using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class WeaponHandler : MonoBehaviour
{
    #region Field & Property
    [SerializeField, ReadOnly] private Player Owner;
    [SerializeField, ReadOnly] private Transform LeftHand;
    [SerializeField, ReadOnly] private Transform RightHand;
    
    [SerializeField, ReadOnly] Weapon PrimaryWeapon;
    [SerializeField, ReadOnly] Weapon SubWeapon;
    [SerializeField, ReadOnly] GameObject PrimaryWeaponModel;
    [SerializeField, ReadOnly] GameObject SubWeaponModel;

    [SerializeField, ReadOnly] private WeaponIndexType _currentWeaponIndex;
    
    public Weapon HoldWeapon
    {
        get
        {
            switch (_currentWeaponIndex)
            {
                case WeaponIndexType.Primary:
                    return PrimaryWeapon;
                case WeaponIndexType.Secondary:
                    return SubWeapon;
                default:
                    return null;
            }
        }
    }
    public GameObject HoldWeaponModel
    {
        get
        {
            switch (_currentWeaponIndex)
            {
                case WeaponIndexType.Primary:
                    return PrimaryWeaponModel;
                case WeaponIndexType.Secondary:
                    return SubWeaponModel;
                default:
                    return null;
            }
        }
    }
    public Weapon FreeWeapon
    {
        get
        {
            switch (_currentWeaponIndex)
            {
                case WeaponIndexType.Primary:
                    return SubWeapon;
                case WeaponIndexType.Secondary:
                    return PrimaryWeapon;
                default:
                    return null;
            }
        }
    }
    public GameObject FreeWeaponModel
    {
        get
        {
            switch (_currentWeaponIndex)
            {
                case WeaponIndexType.Primary:
                    return SubWeaponModel;
                case WeaponIndexType.Secondary:
                    return PrimaryWeaponModel;
                default:
                    return null;
            }
        }
    }
    #endregion
    private void FixedUpdate()
    {
        PrimaryWeapon?.OnUpdate();
        SubWeapon?.OnUpdate();
    }

    public void InitWeaponData(EquipmentData data)
    {
        EquipmentIndexWeapon(data.MainWeapon, ItemSlotType.MainWeapon);
        EquipmentIndexWeapon(data.SubWeapon, ItemSlotType.SubWeapon);
        SetActiveCurrentWeapon();
    }

    //무기를 1, 2번 키로 스왑시
    public void SwapWeapon(WeaponIndexType index)
    {
        if (FreeWeapon == null)
            return;
        if (_currentWeaponIndex == index)
            return;
        _currentWeaponIndex = index;
        SetActiveCurrentWeapon();
    }


    //무기를 인벤토리등에서 변경시
    public void EquipmentIndexWeapon(Item item, ItemSlotType slotType)
    {
        GameData data = DataManager.Instance.GetGameData();
        if (data.ItemData.TryGetValue(item.index, out ItemData itemData))
        {
            GameObject weaponModelPrefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.WeaponAsset, itemData.prefabPathName);
            if (weaponModelPrefab == null)
                return;
            GameObject weaponModel = Object.Instantiate(weaponModelPrefab, RightHand);
            
            switch (slotType)
            {
                case ItemSlotType.MainWeapon:
                    Destroy(PrimaryWeaponModel);
                    PrimaryWeaponModel = weaponModel;
                    if (PrimaryWeapon == null)
                        PrimaryWeapon = new Weapon();
                    PrimaryWeapon.UnInstallWeapon();
                    PrimaryWeapon.InitWeapon(Owner, this, item);
                    break;
                case ItemSlotType.SubWeapon:
                    Destroy(SubWeaponModel);
                    SubWeaponModel = weaponModel;
                    if (SubWeapon == null)
                        SubWeapon = new Weapon();
                    SubWeapon.UnInstallWeapon();
                    SubWeapon.InitWeapon(Owner, this, item);
                    break;
            }
        }
        SetActiveCurrentWeapon();
    }
    
    //현재 무기 활성화, 나머지 무기 비활성화
    private void SetActiveCurrentWeapon()
    {
        if (HoldWeapon != null)
        {
            HoldWeapon.ReceptionHandlerEvent(Trigger.HoldWeapon);
            HoldWeaponModel.gameObject.SetActive(true);
        }

        if (FreeWeapon != null)
        {
            FreeWeapon.ReceptionHandlerEvent(Trigger.FreeWeapon);
            FreeWeaponModel.gameObject.SetActive(false);
        }
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
