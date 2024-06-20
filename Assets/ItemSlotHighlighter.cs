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
        // 마우스가 들어왔을 때의 동작
        if (colorTween != null && colorTween.IsActive())
        {
            colorTween.Kill(); // 이미 실행 중인 트윈이 있다면 중단하고 새로 시작합니다.
        }

        // Dotween을 사용하여 색상 변경을 부드럽게 처리합니다.
        colorTween = imageComponent.DOColor(hoverColor, tweenDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 나갔을 때의 동작
        if (colorTween != null && colorTween.IsActive())
        {
            colorTween.Kill(); // 이미 실행 중인 트윈이 있다면 중단하고 새로 시작합니다.
        }

        // Dotween을 사용하여 색상 변경을 부드럽게 처리합니다.
        colorTween = imageComponent.DOColor(normalColor, tweenDuration);
    }
}
