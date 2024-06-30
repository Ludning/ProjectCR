using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInfoUIHandler : MonoBehaviour
{
    private RectTransform monsterUI;

    public float headUIInterval = 3.0f;
    
    public float minScale = 0.5f; // 최소 스케일
    public float maxScale = 1.5f; // 최대 스케일
    public float minDistance = 5.0f; // 최소 거리
    public float maxDistance = 20.0f; // 최대 거리
    
    private void LateUpdate()
    {
        // 체력바 UI의 위치 설정
        if (monsterUI != null)
        {
            monsterUI.anchoredPosition = GetCanvasPositionFromWorld();
            monsterUI.localScale = GetHealthBarScale();
        }
    }
    
    private void OnBecameVisible()
    {
        Debug.LogWarning($"OnBecameVisible {gameObject.name}");
        Transform ui =
            UIManager.Instance.ShowMonsterUIElement(gameObject, MonsterInfoUIType.Monster, gameObject.GetInstanceID());
        monsterUI = ui.GetComponent<RectTransform>();
    }

    private void OnBecameInvisible()
    {
        Debug.LogWarning($"OnBecameInvisible {gameObject.name}");
        UIManager.Instance.ReturnMonsterUIElement(gameObject);
        monsterUI = null;
    }

    
    private Vector2 GetCanvasPositionFromWorld()
    {
        Vector3 infoUIWorldPosition = GetWorldHealthBarPosition();
        // 몬스터의 월드 좌표를 스크린 좌표로 변환
        Vector3 monsterScreenPos = UIManager.Instance.MainCamera.WorldToScreenPoint(infoUIWorldPosition);
        // 캔버스의 RectTransform을 가져옴
        RectTransform canvasRect = UIManager.Instance.CanvasRectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, monsterScreenPos, null, out var infoUICanvasPosition);
        return infoUICanvasPosition;
    }
    private Vector3 GetWorldHealthBarPosition()
    {
        Vector3 upDir = UIManager.Instance.MainCamera.transform.up;
        return transform.position + upDir * headUIInterval;
    }
    private Vector2 GetHealthBarScale()
    {
        /*// 3. 카메라와 몬스터 간의 거리 계산
        float distance = Vector3.Distance(UIManager.Instance.MainCamera.transform.position, transform.position);

        // 4. 거리 기반 스케일 계산
        float scale = Mathf.Clamp((maxDistance - distance) / (maxDistance - minDistance), minScale, maxScale);*/
        
        float distance = Vector3.Distance(UIManager.Instance.MainCamera.transform.position, transform.position);
        // 거리를 범위 내로 제한
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        // 거리 기반 스케일 계산
        float scale = minScale + (maxScale - minScale) * (maxDistance - distance) / (maxDistance - minDistance);

        // 5. 체력바 UI의 스케일 조절
        return new Vector3(scale, scale, 1);
    }
}
