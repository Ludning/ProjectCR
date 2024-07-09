using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DraggableItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private RectTransform Rect;
	[SerializeField] private Image ItemImage;
	public Transform PreviousParent;

	public ItemElement ItemElement;
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		PreviousParent = transform.parent;

		transform.SetParent(UIManager.Instance.UICanvas.transform); // 부모 오브젝트를 Canvas로 설정
		transform.SetAsLastSibling(); // 가장 앞에 보이도록 마지막 자식으로 설정
		ItemImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Rect.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		DroppableSlotUI droppableSlot = eventData.pointerEnter?.GetComponent<DroppableSlotUI>();
		
		//if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DroppableSlotUI>() == null || eventData.pointerEnter.GetComponent<DroppableSlotUI>().ItemSlot.HasItem == true)
		if (eventData.pointerEnter == null || droppableSlot == null)// || droppableSlot.ItemSlot.HasItem == true)
		{
			ReturnToPreviousSlot();
		}
		ItemImage.raycastTarget = true;
	}

	public void ReturnToPreviousSlot()
	{
		transform.SetParent(PreviousParent);
		Rect.position = PreviousParent.GetComponent<RectTransform>().position;
	}
}

