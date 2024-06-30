using System;
using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.X509.Qualified;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = System.Object;

public class ResourceManager : SingleTon<ResourceManager>
{
    /*
    private Dictionary<string, GameObject> prefabResource = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> spriteResource = new Dictionary<string, Sprite>();
    private Dictionary<string, AudioClip> audioResource = new Dictionary<string, AudioClip>();
    */
    
    private Dictionary<Type, Dictionary<string, Object>> _resourceDictionary = new Dictionary<Type, Dictionary<string, Object>>();
    public T LoadResource<T>(AssetAddressType type, string addressKey)
    {
        string address = GetAddressFromDataManager(type, addressKey);
        T resource = Addressables.LoadAssetAsync<T>(address).WaitForCompletion();
        if (resource == null)
            throw new System.NotImplementedException();
        return resource;
    }
    public T LoadResourceWithCaching<T>(AssetAddressType type, string addressKey)
    {
        if(!_resourceDictionary.ContainsKey(typeof(T)))
            _resourceDictionary.Add(typeof(T), new Dictionary<string, Object>());
        return LoadResourceWithCaching<T>(type, addressKey, _resourceDictionary[typeof(T)]);
        
        /*
        if (typeof(T) == typeof(GameObject))
            resourceDict = prefabResource as Dictionary<string, T>;
        else if (typeof(T) == typeof(Sprite))
            resourceDict = spriteResource as Dictionary<string, T>;
        else if (typeof(T) == typeof(AudioClip))
            resourceDict = audioResource as Dictionary<string, T>;
        else
            return default(T);
        return LoadResourceWithCaching<T>(addressKey, resourceDict);*/
    }
    
    private T LoadResourceWithCaching<T>(AssetAddressType type, string addressKey, Dictionary<string, Object> resourceDict)
    {
        if(!resourceDict.ContainsKey(addressKey))
            resourceDict.Add(addressKey, LoadResource<T>(type, addressKey));
        return (T)resourceDict[addressKey];
        
        /*T resource;
        if (resourceDict.TryGetValue(addressKey, out resource))
            return resource;
    
        resource = LoadResource<T>(addressKey);
    
        resourceDict.Add(addressKey, resource);
        return resource;*/
    }

    private string GetAddressFromDataManager(AssetAddressType type, string addressKey)
    {
        return DataManager.Instance.GetPrefabAddress(type, addressKey);
    }
}
