using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIRenderQueue : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    public void OnSetSort()
    {
        UI.GetComponent<Renderer>().sortingOrder = 3;
    }
}
