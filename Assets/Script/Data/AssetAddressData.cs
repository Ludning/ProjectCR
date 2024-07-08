using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetAddressData", menuName = "Data/AssetAddressData")]
public class AssetAddressData : SerializedScriptableObject
{
    public Dictionary<string, AssetData> UICoreAsset;
    public Dictionary<string, AssetData> GameUIAsset;
    public Dictionary<string, AssetData> MiddleUIAsset;
    public Dictionary<string, AssetData> PopupUIAsset;
    public Dictionary<string, AssetData> ElementUIAsset;
    public Dictionary<string, AssetData> SpriteAsset;
    
    
    public Dictionary<string, AssetData> CharacterAsset;
    public Dictionary<string, AssetData> AnimalAsset;
    
    public Dictionary<string, AssetData> WeaponAsset;
    public Dictionary<string, AssetData> SpawnableAsset;
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
            case AssetAddressType.ElementUIAsset:
                if (ElementUIAsset.ContainsKey(key))
                    return ElementUIAsset[key].path;
                break;
            case AssetAddressType.SpriteAsset:
                if (SpriteAsset.ContainsKey(key))
                    return SpriteAsset[key].path;
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
            case AssetAddressType.SpawnableAsset:
                if (SpawnableAsset.ContainsKey(key))
                    return SpawnableAsset[key].path;
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
public class AssetData : IParserable
{
    [TableColumnWidth(1)]
    public string name;
    public string path;
    
    public static void SetParserData<T>(Dictionary<string, int> columnTypeDic, DataRow dataRow, T dataInstance) where T : IParserable
    {
        // 변수의 이름을 가져오기 위해 변수가 있는 클래스의 타입을 알아야 합니다.
        foreach (KeyValuePair<string,int> keyValuePair in columnTypeDic)
        {
            FieldInfo fieldInfo = typeof(T).GetField(keyValuePair.Key, BindingFlags.Public | BindingFlags.Instance);
            Type fieldType = fieldInfo.FieldType;
            
            if(dataRow[keyValuePair.Value] == DBNull.Value)
                continue;
            StringParserHelper.BuiltInTypeParser<T>(dataInstance, fieldType, fieldInfo.Name, dataRow[keyValuePair.Value]);
        }
    }
}
