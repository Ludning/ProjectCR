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
    
    
    public Dictionary<string, AssetData> PopupUIAsset;
    public Dictionary<string, AssetData> CharacterAsset;
    public Dictionary<string, AssetData> AnimalAsset;

    public string GetAddressPath(AssetAddressType type)
    {
        string path = "";
        switch (type)
        {
            case AssetAddressType.PopupUIAsset:
                break;
            case AssetAddressType.CharacterAsset:
                break;
            case AssetAddressType.AnimalAsset:
                break;
        }

        return path;
    }
}

[Serializable]
public class AssetData
{
    [TableColumnWidth(1)]
    public string name;
    public string path;
}
