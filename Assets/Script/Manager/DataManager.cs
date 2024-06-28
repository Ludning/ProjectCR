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
    
    public string GetPrefabAddress(string name)
    {
        //TODO
        //AssetAddressData.
        return null;
    }
    
    public void GetGameData(string name)
    {
        return ;
    }

    
}
