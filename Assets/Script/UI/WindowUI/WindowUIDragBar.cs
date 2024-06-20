using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowUIDragBar : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private WindowUIBase WindowUI;
    [SerializeField] private RectTransform WindowUITransform;
    private Vector2 offset = Vector2.zero;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)WindowUITransform.parent, 
            eventData.position, 
            eventData.pressEventCamera, 
            out localPoint
        );

        // 새로운 위치를 설정합니다.
        WindowUITransform.localPosition = localPoint - offset;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        WindowUI.OnHierarchyMoveToLast();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            WindowUITransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out offset
        );
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        if (!IsWithinScreen(screenSize))
        {
            MoveToNearestCorner(screenSize);
        }
    }
    private bool IsWithinScreen(Vector2 screenSize)
    {
        Vector3[] corners = new Vector3[4];
        WindowUITransform.GetWorldCorners(corners);

        foreach (Vector3 corner in corners)
        {
            if (corner.x < 0 || corner.x > screenSize.x || corner.y < 0 || corner.y > screenSize.y)
            {
                return false;
            }
        }
        return true;
    }

    private void MoveToNearestCorner(Vector2 screenSize)
    {
        Vector3[] corners = new Vector3[4];
        WindowUITransform.GetWorldCorners(corners);

        //좌하단 (좌우체크)
        if (corners[0].x < 0)
        {
            Debug.Log(corners[0].x);
            WindowUITransform.transform.position -= new Vector3(corners[0].x, 0, 0);
        }
        //우상단 (좌우체크)
        else if (corners[2].x > screenSize.x)
        {
            Debug.Log(corners[2].x - screenSize.x);
            WindowUITransform.transform.position -= new Vector3(corners[2].x - screenSize.x, 0, 0);
        }
        //좌하단 (상하체크)
        if (corners[0].y < 0)
        {
            Debug.Log(corners[0].y);
            WindowUITransform.transform.position -= new Vector3(0, corners[0].y, 0);
        }
        //우상단 (상하체크)
        else if (corners[2].y > screenSize.y)
        {
            Debug.Log(corners[2].y - screenSize.y);
            WindowUITransform.transform.position -= new Vector3(0, corners[2].y - screenSize.y, 0);
        }
    }
}
