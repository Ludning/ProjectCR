using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    private Canvas _uiCanvas;
    private Transform _gameUI;
    private Transform _popupUI;

    private Dictionary<GameUIElementType, Transform> GameUIDic = new Dictionary<GameUIElementType, Transform>();
    private Dictionary<PopupUIElementType, Transform> PopupUIDic = new Dictionary<PopupUIElementType, Transform>();

    public Canvas UICanvas
    {
        get
        {
            if (_uiCanvas == null)
            {
                _uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            return _uiCanvas;
        }
    }
    public Transform GameUI
    {
        get
        {
            if (_gameUI == null)
            {
                _gameUI = UICanvas.transform.Find("GameUI");
            }
            return _gameUI;
        }
    }
    public Transform PopupUI
    {
        get
        {
            if (_popupUI == null)
            {
                _popupUI = UICanvas.transform.Find("PopupUI");
            }
            return _popupUI;
        }
    }

    public Transform ShowGameUIElement(GameUIElementType elementType)
    {
        if (GameUI == null)
            return null;
        if (!GameUIDic.ContainsKey(elementType))
        {
            Transform gameUIElement = GameUI.Find(elementType.ToString());
            if (gameUIElement == null)
            {
                GameObject prefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(elementType.ToString());
                gameUIElement = Object.Instantiate(prefab, GameUI).transform;
            }
            GameUIDic.Add(elementType, gameUIElement);
        }
        return GameUIDic[elementType];
    }

    public Transform ShowPopupUIElement(PopupUIElementType elementType)
    {
        if (PopupUI == null)
            return null;
        if (!PopupUIDic.ContainsKey(elementType))
        {
            Transform popupUIElement = PopupUI.Find(elementType.ToString());
            if (popupUIElement == null)
            {
                GameObject prefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(elementType.ToString());
                popupUIElement = Object.Instantiate(prefab, PopupUI).transform;
            }
            PopupUIDic.Add(elementType, popupUIElement);
        }
        return PopupUIDic[elementType];
    }
}
