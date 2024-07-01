using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInfoUIHandler : InfoUIHandler
{
    protected override void OnVisibleInstantiateUI()
    {
        Debug.LogWarning($"InstantiateUI {gameObject.name}");
        Transform ui =
            UIManager.Instance.ShowMiddleUIElement(gameObject, MiddleUIType.Monster, gameObject.GetInstanceID());
        _uiRect = ui.GetComponent<RectTransform>();
    }

    protected override void OnInvisibleReturnToPoolUI()
    {
        Debug.LogWarning($"OnBecameInvisible {gameObject.name}");
        UIManager.Instance.ReturnMonsterUIElement(gameObject);
        _uiRect = null;
    }
}
