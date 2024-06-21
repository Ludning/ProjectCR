using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DraggableItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private RectTransform Rect;
	[SerializeField] private Image ItemImage;
	private Transform PreviousParent;
	
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
		// 드래그 직전에 소속되어 있던 아이템 슬롯으로 아이템 이동
		//if (transform.parent == canvas)
		if(eventData.pointerEnter == null ||eventData.pointerEnter.GetComponent<DroppableSlotUI>() == null)
		{
			transform.SetParent(PreviousParent);
			Rect.position = PreviousParent.GetComponent<RectTransform>().position;
		}
		ItemImage.raycastTarget = true;
	}
}

