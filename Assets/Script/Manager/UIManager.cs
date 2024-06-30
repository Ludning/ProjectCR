using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    private Camera _mainCamera;
    private Canvas _uiCanvas;
    private RectTransform _canvasRectTransform;
    private Transform _gameUIParent;
    private Transform _monsterUIParent;
    private Transform _popupUIParent;

    private Dictionary<GameUIElementType, Transform> GameUIDic = new Dictionary<GameUIElementType, Transform>();
    private Dictionary<GameObject, Transform> MonsterUIDic = new Dictionary<GameObject, Transform>();
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
                _uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            return _uiCanvas;
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
    public Transform MonsterUIParent
    {
        get
        {
            if (_monsterUIParent == null)
            {
                _monsterUIParent = UICanvas.transform.Find("MonsterUI");
            }
            return _monsterUIParent;
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
                GameObject prefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.GameUIAsset, elementType.ToString());
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
                GameObject prefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.PopupUIAsset, elementType.ToString());
                popupUIElement = PoolManager.Instance.GetGameObject(prefab).transform;
                popupUIElement.SetParent(PopupUIParent);
            }
            PopupUIDic.Add(elementType, popupUIElement);
        }
        return PopupUIDic[elementType];
    }
    
    //중복되는 Type의 여러 UI인스턴스가 있으므로 owner를 key로 저장
    public Transform ShowMonsterUIElement(GameObject owner, MonsterInfoUIType monsterUIType, int instanceID)
    {
        if (MonsterUIParent == null)
            return null;
        if (!MonsterUIDic.ContainsKey(owner))
        {
            //Transform monsterUI = MonsterUIParent.Find(monsterUIType.ToString());

            //TODO
            //DataManager.Instance.GetPrefabAddress();
            GameObject prefab = ResourceManager.Instance.LoadResourceWithCaching<GameObject>(AssetAddressType.MonsterUIAsset, monsterUIType.ToString());
            Transform monsterUI = PoolManager.Instance.GetGameObject(prefab).transform;
            monsterUI.SetParent(MonsterUIParent);
            monsterUI.GetComponent<MonsterInfo_View>().SetId(instanceID);
            MonsterUIDic.Add(owner, monsterUI);
        }

        return MonsterUIDic[owner];
    }
    public void ReturnMonsterUIElement(GameObject owner)
    {
        if (MonsterUIDic.TryGetValue(owner, out Transform ui)) //.ContainsKey(owner))
        {
            if (ui != null)
                PoolManager.Instance.ReturnToPool(ui.gameObject); //pool로 반환
            MonsterUIDic.Remove(owner);
        }
    }
}
