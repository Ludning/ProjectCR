using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUIHandler : InfoUIHandler
{
    [SerializeField] private Player PlayerComponent;
    
    protected override void OnVisibleInstantiateUI()
    {
        Debug.LogWarning($"PlayerUI_Visible {gameObject.name}");
        Transform ui =
            UIManager.Instance.ShowMiddleUIElement(gameObject, MiddleUIType.PlayerInfo, gameObject.GetInstanceID());
        _uiRect = ui.GetComponent<RectTransform>();

        InitUIData();
    }

    protected override void OnInvisibleReturnToPoolUI()
    {
        Debug.LogWarning($"PlayerUI_Invisible {gameObject.name}");
        UIManager.Instance.ReturnMiddleUIElement(gameObject);
        _uiRect = null;
    }

    protected override void InitUIData()
    {
        _uiRect.GetComponent<PlayerInfo_View>().SetId(gameObject.GetInstanceID());

        PlayerInfo_Message msg = new PlayerInfo_Message()
        {
            ID = gameObject.GetInstanceID(),
            Level = PlayerComponent.GetLevel(),
            NickName = PlayerComponent.GetNickName()
        };
        MessageManager.Instance.InvokeCallback(msg);
    }
}
