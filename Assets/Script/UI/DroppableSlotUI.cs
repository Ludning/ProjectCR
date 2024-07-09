using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableSlotUI : MonoBehaviour, IDropHandler
{
	[SerializeField] private RectTransform rect;
	[SerializeField] private Image image;

	public ItemSlot ItemSlot;

	public void OnDrop(PointerEventData eventData)
	{
		DraggableItemUI draggableItemUI = eventData.pointerDrag?.GetComponent<DraggableItemUI>();
		if (eventData.pointerDrag == null || draggableItemUI == null)
			return;
		
		if (ItemSlot.HasItem == true)
		{
			draggableItemUI.ReturnToPreviousSlot();
		}
		else
		{
			ItemSlot prevSlot = draggableItemUI.PreviousParent.GetComponent<ItemSlot>();
			prevSlot.Item = null;
			draggableItemUI.transform.SetParent(transform);
			draggableItemUI.GetComponent<RectTransform>().position = rect.position;
			ItemSlot.Item = draggableItemUI.GetComponent<ItemElement>();

			WepServerConnectionManager.Instance.RequestItemSlotChange(prevSlot.SlotType, prevSlot.Index, ItemSlot.SlotType, ItemSlot.Index);
		}
	}
}

