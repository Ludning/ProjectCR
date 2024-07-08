using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

public class Inventory_View : ViewBase<Inventory_ViewModel, Inventory_Message>
{
    [SerializeField]
    private List<Transform> itemSlotList;
    
    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "InventoryData")
        {
            GameData data = DataManager.Instance.GetGameData();
            GameObject itemPrefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.ElementUIAsset, "ItemIcon");
            foreach (var keyValueItemData in PlayerManager.Instance.InventoryData.ItemDictionary)
            {
                if (data.ItemData.TryGetValue(keyValueItemData.Value.index, out ItemData itemData))
                {
                    Sprite itemSprite =
                        ResourceManager.Instance.LoadResource<Sprite>(AssetAddressType.AnimalAsset,
                            itemData.iconPathName);
                    Transform itemObject;
                    if (itemSlotList[keyValueItemData.Key].childCount == 1)
                    {
                        itemObject = itemSlotList[keyValueItemData.Key].GetChild(0);
                    }
                    else
                    {
                        itemObject = PoolManager.Instance.GetGameObject(itemPrefab).transform;
                        itemObject.SetParent(itemSlotList[keyValueItemData.Key], false);
                    }

                    ItemElement itemElement = itemObject.GetComponent<ItemElement>();
                    itemElement.SetImage(itemSprite);

                }
            }
        }
    }
}
