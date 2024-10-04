using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowPopupUIButton : MonoBehaviour
{
    public PopupUIElementType popupUIElementType;
    public void OnClickEvent()
    {
        UIManager.Instance.ShowPopupUIElement(popupUIElementType);
    }
}
