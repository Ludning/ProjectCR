using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalModel : MonoBehaviour
{
    private RectTransform monsterUI;
    private void LateUpdate()
    {
        // 몬스터의 월드 좌표를 스크린 좌표로 변환
        Vector3 monsterScreenPos = UIManager.Instance.MainMainCamera.WorldToScreenPoint(transform.position);
        // 캔버스의 RectTransform을 가져옴
        RectTransform canvasRect = UIManager.Instance.CanvasRectTransform;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, monsterScreenPos, null, out var healthBarPosition);
        // 체력바 UI의 위치 설정
        monsterUI.anchoredPosition = healthBarPosition;
    }

    void OnBecameVisible()
    {
         Transform ui = UIManager.Instance.ShowMonsterUIElement(gameObject, MonsterInfoUIType.Monster, gameObject.GetInstanceID());
         monsterUI = ui.GetComponent<RectTransform>();
    }
    void OnBecameInvisible()
    {
        UIManager.Instance.ReturnMonsterUIElement(gameObject);
        monsterUI = null;
    }
}
