using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableSlotUI : MonoBehaviour, IDropHandler
{
	[SerializeField] private Image image;
	[SerializeField] private RectTransform rect;

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableItemUI>() != null)
		{
			eventData.pointerDrag.transform.SetParent(transform);
			eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
		}
	}
}

