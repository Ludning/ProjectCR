using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetAddressData", menuName = "Data/AssetAddressData")]
public class AssetAddressData : ScriptableObject
{
    public List<AssetData> PupupUIAsset;
    public List<AssetData> CharacterAsset;
    public List<AssetData> AnimalAsset;
}

[Serializable]
public struct AssetData
{
    [TableColumnWidth(1)]
    public string name;
    public string path;
}
