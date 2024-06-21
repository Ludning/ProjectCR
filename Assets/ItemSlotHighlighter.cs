using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white; // 기본 색상
    public Color hoverColor = Color.red;   // 마우스 오버 시 색상
    public float tweenDuration = 0.2f;     // Dotween 트윈 지속 시간
    [SerializeField] private Image imageComponent;
    private Tweener colorTween;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (colorTween != null && colorTween.IsActive())
            colorTween.Kill();
        colorTween = imageComponent.DOColor(hoverColor, tweenDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (colorTween != null && colorTween.IsActive())
            colorTween.Kill();
        colorTween = imageComponent.DOColor(normalColor, tweenDuration);
    }
}
