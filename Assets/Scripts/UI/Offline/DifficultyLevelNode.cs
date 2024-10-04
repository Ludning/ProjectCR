using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultyLevelNode : MonoBehaviour, IPointerClickHandler
{
    public event Action<DifficultyLevelNode> OnClickEvent;

    public Image BackgroundImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
}
