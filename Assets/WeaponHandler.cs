using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class WeaponHandler : MonoBehaviour
{
    #region Field & Property
    [SerializeField] private Player Owner;
    [SerializeField, ReadOnly] private Transform LeftHand;
    [SerializeField, ReadOnly] private Transform RightHand;
    
    [SerializeField, ReadOnly] GameObject PrimaryWeaponModel = null;
    [SerializeField, ReadOnly] GameObject SubWeaponModel = null;

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _overrideProjectilePrefab = null;
    
    
    public GameObject HoldWeaponModel
    {
        get
        {
            switch (PlayerManager.Instance.CurrentWeaponIndex)
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
    
    public GameObject FreeWeaponModel
    {
        get
        {
            switch (PlayerManager.Instance.CurrentWeaponIndex)
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

    public void SetActiveModel()
    {
        if(HoldWeaponModel!=null)
            HoldWeaponModel.SetActive(true);
        if(FreeWeaponModel!=null)
            FreeWeaponModel.SetActive(false);
        
        switch (PlayerManager.Instance.CurrentWeaponIndex)
        {
            case WeaponIndexType.Primary:
                OnInstallWeaponAnimation(PlayerManager.Instance.EquipmentDatas.MainWeapon);
                break;
            case WeaponIndexType.Secondary:
                OnInstallWeaponAnimation(PlayerManager.Instance.EquipmentDatas.SubWeapon);
                break;
        }
    }
    
    //무기를 인벤토리등에서 변경시
    public void EquipmentIndexWeapon(Item item, EquipmentSlotType slotType)
    {
        GameData data = DataManager.Instance.GetGameData();
        if (data.ItemData.TryGetValue(item.index, out ItemData itemData))
        {
            if (itemData.index == 0)
            {
                switch (slotType)
                {
                    case EquipmentSlotType.MainWeapon:
                        if(PrimaryWeaponModel != null)
                            Destroy(PrimaryWeaponModel);
                        break;
                    case EquipmentSlotType.SubWeapon:
                        if(SubWeaponModel != null)
                            Destroy(SubWeaponModel);
                        break;
                }
                return;
            }
            GameObject weaponModelPrefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.WeaponAsset, itemData.prefabPathName);
            if (weaponModelPrefab == null)
                return;
            GameObject weaponModel = Object.Instantiate(weaponModelPrefab, RightHand);
            switch (slotType)
            {
                case EquipmentSlotType.MainWeapon:
                    if(PrimaryWeaponModel != null)
                        Destroy(PrimaryWeaponModel);
                    PrimaryWeaponModel = weaponModel;
                    break;
                case EquipmentSlotType.SubWeapon:
                    if(SubWeaponModel != null)
                        Destroy(SubWeaponModel);
                    SubWeaponModel = weaponModel;
                    break;
            }
        }
    }
    //무기 애니메이션 적용
    private void OnInstallWeaponAnimation(Item item)
    {
        if (item.index == 0)
        {
            
        }
        else
        {
            ItemData data = DataManager.Instance.GetGameData().ItemData[item.index];
            
            if(PlayerManager.Instance.Player != null)
                PlayerManager.Instance.Player.animatorController.SetAnimationClipByWeaponType(StateType.Attack, data.weaponType);
        }
        
        //_animator = ;
        //animatorController.SetAnimationClipByWeaponType();
    }
    public void SetOverrideProjectilePrefab(string prefabName = "")
    {
        if (string.IsNullOrWhiteSpace(prefabName))
        {
            _overrideProjectilePrefab = null;
        }
        else
        {
            GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.SpawnableAsset, prefabName);
            _overrideProjectilePrefab = prefab;
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
