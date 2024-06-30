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
    
    
    public Dictionary<string, AssetData> GameUIAsset;
    public Dictionary<string, AssetData> PopupUIAsset;
    public Dictionary<string, AssetData> CharacterAsset;
    public Dictionary<string, AssetData> MonsterUIAsset;
    public Dictionary<string, AssetData> AnimalAsset;

    public string GetAddressPath(AssetAddressType type, string key)
    {
        switch (type)
        {
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
            case AssetAddressType.MonsterUIAsset:
                if (MonsterUIAsset.ContainsKey(key))
                    return MonsterUIAsset[key].path;
                break;
            case AssetAddressType.AnimalAsset:
                if (AnimalAsset.ContainsKey(key))
                    return AnimalAsset[key].path;
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
