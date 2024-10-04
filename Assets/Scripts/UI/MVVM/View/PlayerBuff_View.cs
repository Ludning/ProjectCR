using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;

public class PlayerBuff_View : ViewBase<PlayerBuff_ViewModel, Record_Message>
{
    private Dictionary<string, BuffUIElement> BuffUIElements = new Dictionary<string, BuffUIElement>();

    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_vm.BuffDictionary.TryGetValue(e.PropertyName, out int value))
        {
            if (BuffUIElements.ContainsKey(e.PropertyName) == false)
            {
                GameObject buffUIElementPrefab =
                    ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.ElementUIAsset, "BuffUIElement");
                GameObject buffUIElement = PoolManager.Instance.GetGameObject(buffUIElementPrefab, transform);
                BuffUIElements.Add(e.PropertyName, buffUIElement.GetComponent<BuffUIElement>());
            }
            BuffUIElements[e.PropertyName].SetBuffUI(e.PropertyName, value.ToString());
        }
        else
        {
            if (BuffUIElements.ContainsKey(e.PropertyName))
            {
                BuffUIElement tempBuffUI = BuffUIElements[e.PropertyName];
                BuffUIElements.Remove(e.PropertyName);
                PoolManager.Instance.ReturnToPool(tempBuffUI.gameObject);
            }
        }
        /*if (_vm.BuffDictionary.TryGetValue(e.PropertyName, out int value))
        {
            if (value == 0)
            {
                if (BuffUIElements.ContainsKey(e.PropertyName))
                {
                    BuffUIElement tempBuffUI = BuffUIElements[e.PropertyName];
                    BuffUIElements.Remove(e.PropertyName);
                    PoolManager.Instance.ReturnToPool(tempBuffUI.gameObject);
                }
            }
            else
            {
                if (BuffUIElements.TryGetValue(e.PropertyName, out BuffUIElement uiElement))
                {
                    uiElement.SetBuffUI(e.PropertyName, value.ToString());
                }
                else
                {
                    GameObject buffUIElementPrefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.ElementUIAsset, "BuffUIElement");
                    GameObject buffUIElement = PoolManager.Instance.GetGameObject(buffUIElementPrefab, transform);
                    BuffUIElements.Add(e.PropertyName, buffUIElement.GetComponent<BuffUIElement>());
                    BuffUIElements[e.PropertyName].SetBuffUI(e.PropertyName, value.ToString());
                }
            }
        }
        else
        {
            if (BuffUIElements.ContainsKey(e.PropertyName))
            {
                BuffUIElement tempBuffUI = BuffUIElements[e.PropertyName];
                BuffUIElements.Remove(e.PropertyName);
                PoolManager.Instance.ReturnToPool(tempBuffUI.gameObject);
            }
        }*/
    }
}
