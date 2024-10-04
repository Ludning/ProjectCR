using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SpawnObject : EffectModule
{
    private Mediator _mediator;

    [SerializeField, ReadOnly]
    private string _objectName;
    public override void InitData(string effectData, Mediator mediator)
    {
        _mediator = mediator;

        _objectName = effectData;
    }

    public override void InvokeEffect()
    {
        GameObject prefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.SpawnableAsset, _objectName);
        PoolManager.Instance.GetGameObject(prefab, PlayerManager.Instance.Player.transform.position);
    }

    public override void CancelEffect()
    {
        
    }
}
