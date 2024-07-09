using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffUIElement : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Value;

    public void SetBuffUI(string name, string value)
    {
        Name.text = name;
        Value.text = value;
    }
}
