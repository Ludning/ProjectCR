using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUIHandler : InfoUIHandler
{
    protected override void OnVisibleInstantiateUI()
    {
        Debug.LogWarning($"PlayerUI_Visible {gameObject.name}");
        Transform ui =
            UIManager.Instance.ShowMiddleUIElement(gameObject, MiddleUIType.Monster, gameObject.GetInstanceID());
        _uiRect = ui.GetComponent<RectTransform>();
    }

    protected override void OnInvisibleReturnToPoolUI()
    {
        Debug.LogWarning($"PlayerUI_Invisible {gameObject.name}");
        UIManager.Instance.ReturnMonsterUIElement(gameObject);
        _uiRect = null;
    }
}
