using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

public class UIManager : SingleTon<UIManager>
{
    private Camera _mainCamera;
    private Canvas _uiCanvas;
    private EventSystem _uiEventSystem;
    private RectTransform _canvasRectTransform;
    private Transform _gameUIParent;
    private Transform _middleUIParent;
    private Transform _popupUIParent;

    private Dictionary<GameUIElementType, Transform> GameUIDic = new Dictionary<GameUIElementType, Transform>();
    private Dictionary<GameObject, Transform> MiddleUIDic = new Dictionary<GameObject, Transform>();
    private Dictionary<PopupUIElementType, Transform> PopupUIDic = new Dictionary<PopupUIElementType, Transform>();

    public Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            return _mainCamera;
        }
    }

    public Canvas UICanvas
    {
        get
        {
            if (_uiCanvas == null)
            {
                //_uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                GameObject canvasObject = GameObject.Find(UICoreType.Canvas.ToString());
                if (canvasObject == null)
                {
                    GameObject canvasPrefab = ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.UICoreAsset, UICoreType.Canvas.ToString());
                    canvasObject = Object.Instantiate(canvasPrefab);
                }
                _uiCanvas = canvasObject.GetComponent<Canvas>();
                if (UIEventSystem != null)
                {
                    Debug.Log("UIEventSystem 활성화");
                }
            }
            return _uiCanvas;
        }
    }

    public EventSystem UIEventSystem
    {
        get
        {
            if (_uiEventSystem == null)
            {
                GameObject eventSystemObject = GameObject.Find(UICoreType.EventSystem.ToString());
                if (eventSystemObject == null)
                {
                    GameObject eventSystemPrefab =
                        ResourceManager.Instance.LoadResource<GameObject>(AssetAddressType.UICoreAsset,
                            UICoreType.EventSystem.ToString());
                    eventSystemObject = Object.Instantiate(eventSystemPrefab);
                }
                _uiEventSystem = eventSystemObject.GetComponent<EventSystem>();
            }
            return _uiEventSystem;
        }
    }

    public RectTransform CanvasRectTransform
    {
        get
        {
            if (_canvasRectTransform == null)
            {
                _canvasRectTransform = UICanvas.GetComponent<RectTransform>();
            }

            return _canvasRectTransform;
        }
    }

    public Transform GameUIParent
    {
        get
        {
            if (_gameUIParent == null)
            {
                _gameUIParent = UICanvas.transform.Find("GameUI");
            }

            return _gameUIParent;
        }
    }

    public Transform MiddleUIParent
    {
        get
        {
            if (_middleUIParent == null)
            {
                _middleUIParent = UICanvas.transform.Find("MiddleUI");
            }

            return _middleUIParent;
        }
    }

    public Transform PopupUIParent
    {
        get
        {
            if (_popupUIParent == null)
            {
                _popupUIParent = UICanvas.transform.Find("PopupUI");
            }

            return _popupUIParent;
        }
    }


    public Transform ShowGameUIElement(GameUIElementType elementType)
    {
        if (GameUIParent == null)
            return null;
        if (!GameUIDic.ContainsKey(elementType))
        {
            Transform gameUIElement = GameUIParent.Find(elementType.ToString());
            if (gameUIElement == null)
            {
                GameObject prefab =
                    ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.GameUIAsset,
                        elementType.ToString());
                gameUIElement = PoolManager.Instance.GetGameObject(prefab).transform;
                gameUIElement.SetParent(GameUIParent);
            }

            GameUIDic.Add(elementType, gameUIElement);
        }

        return GameUIDic[elementType];
    }

    public Transform ShowPopupUIElement(PopupUIElementType elementType)
    {
        if (PopupUIParent == null)
            return null;
        if (!PopupUIDic.ContainsKey(elementType))
        {
            Transform popupUIElement = PopupUIParent.Find(elementType.ToString());
            if (popupUIElement == null)
            {
                GameObject prefab =
                    ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.PopupUIAsset,
                        elementType.ToString());
                popupUIElement = Object.Instantiate(prefab).transform;//PoolManager.Instance.GetGameObject(prefab).transform;
                popupUIElement.SetParent(PopupUIParent, false);
            }

            PopupUIDic.Add(elementType, popupUIElement);
        }
        PopupUIDic[elementType].gameObject.SetActive(true);
        return PopupUIDic[elementType];
    }

    //중복되는 Type의 여러 UI인스턴스가 있으므로 owner를 key로 저장
    public Transform ShowMiddleUIElement(GameObject owner, MiddleUIType middleUIType, int instanceID)
    {
        if (MiddleUIParent == null)
            return null;
        if (!MiddleUIDic.ContainsKey(owner))
        {
            //Transform monsterUI = MonsterUIParent.Find(monsterUIType.ToString());

            //TODO
            //DataManager.Instance.GetPrefabAddress();
            GameObject prefab =
                ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.MiddleUIAsset,
                    middleUIType.ToString());
            Transform middleUI = PoolManager.Instance.GetGameObject(prefab).transform;
            middleUI.SetParent(MiddleUIParent);
            
            ISetAbleMiddleViewId middleViewId = middleUI.GetComponent<ISetAbleMiddleViewId>();
            if(middleViewId != null)
                middleViewId.SetId(instanceID);
            
            MiddleUIDic.Add(owner, middleUI);
        }
        return MiddleUIDic[owner];
    }

    public void ReturnMiddleUIElement(GameObject owner)
    {
        if (MiddleUIDic.TryGetValue(owner, out Transform ui)) //.ContainsKey(owner))
        {
            if (ui != null)
                PoolManager.Instance.ReturnToPool(ui.gameObject); //pool로 반환
            MiddleUIDic.Remove(owner);
        }
    }
}