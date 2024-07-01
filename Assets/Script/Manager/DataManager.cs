using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    const string addressAssetPath = "Assets/Resource/Data/AssetAddress.asset";
    const string dataAssetPath = "Assets/Resource/Data/GameData.asset";
    
    private AssetAddressData assetAddressData;
    private GameData gameData;
    
    private AssetAddressData AssetAddressData
    {
        get
        {
            if (assetAddressData == null)
                assetAddressData = AssetDatabase.LoadAssetAtPath<AssetAddressData>(addressAssetPath);
            return assetAddressData;
        }
    }
    private GameData GameData
    {
        get
        {
            if (gameData == null)
                gameData = AssetDatabase.LoadAssetAtPath<GameData>(dataAssetPath);
            return gameData;
        }
    }
    
    public string GetPrefabAddress(AssetAddressType type, string key)
    {
        return AssetAddressData.GetAddressPath(type, key);
    }
    
    public GameData GetGameData()
    {
        return GameData;
    }

    
}
