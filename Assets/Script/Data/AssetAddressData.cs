using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetAddressData", menuName = "Data/AssetAddressData")]
public class AssetAddressData : SerializedScriptableObject
{
    //public List<AssetData> PopupUIAsset;
    //public List<AssetData> CharacterAsset;
    //public List<AssetData> AnimalAsset;
    
    
    public Dictionary<string, AssetData> UICoreAsset;
    public Dictionary<string, AssetData> GameUIAsset;
    public Dictionary<string, AssetData> PopupUIAsset;
    public Dictionary<string, AssetData> CharacterAsset;
    public Dictionary<string, AssetData> MiddleUIAsset;
    public Dictionary<string, AssetData> AnimalAsset;
    
    public Dictionary<string, AssetData> WeaponAsset;
    public Dictionary<string, AssetData> WeaponAnimationClipAsset;
    public Dictionary<string, AssetData> SkillAnimationClipAsset;

    public string GetAddressPath(AssetAddressType type, string key)
    {
        switch (type)
        {
            case AssetAddressType.UICoreAsset:
                if (UICoreAsset.ContainsKey(key))
                    return UICoreAsset[key].path;
                break;
            case AssetAddressType.GameUIAsset:
                if (GameUIAsset.ContainsKey(key))
                    return GameUIAsset[key].path;
                break;
            case AssetAddressType.PopupUIAsset:
                if (PopupUIAsset.ContainsKey(key))
                    return PopupUIAsset[key].path;
                break;
            case AssetAddressType.CharacterAsset:
                if (CharacterAsset.ContainsKey(key))
                    return CharacterAsset[key].path;
                break;
            case AssetAddressType.MiddleUIAsset:
                if (MiddleUIAsset.ContainsKey(key))
                    return MiddleUIAsset[key].path;
                break;
            case AssetAddressType.AnimalAsset:
                if (AnimalAsset.ContainsKey(key))
                    return AnimalAsset[key].path;
                break;
            
            case AssetAddressType.WeaponAsset:
                if (WeaponAsset.ContainsKey(key))
                    return WeaponAsset[key].path;
                break;
            case AssetAddressType.WeaponAnimationClipAsset:
                if (WeaponAnimationClipAsset.ContainsKey(key))
                    return WeaponAnimationClipAsset[key].path;
                break;
            case AssetAddressType.SkillAnimationClipAsset:
                if (SkillAnimationClipAsset.ContainsKey(key))
                    return SkillAnimationClipAsset[key].path;
                break;
        }
        return null;
    }
}

[Serializable]
public class AssetData
{
    [TableColumnWidth(1)]
    public string name;
    public string path;
}
