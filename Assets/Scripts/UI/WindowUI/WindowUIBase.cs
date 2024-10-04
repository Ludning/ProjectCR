using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowUIBase : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnHierarchyMoveToLast();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }
    public void OnDrag(PointerEventData eventData)
    {
    }
    public void OnEndDrag(PointerEventData eventData)
    {
    }
    public void OnHierarchyMoveToLast()
    {
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }
}
