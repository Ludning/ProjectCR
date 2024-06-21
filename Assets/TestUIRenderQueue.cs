using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIRenderQueue : MonoBehaviour
{
    public void OnSetSort()
    {
        UIManager.Instance.ShowPopupUIElement(PopupUIElementType.WindowUIBase);
    }
}
