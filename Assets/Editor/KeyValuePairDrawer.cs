using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

[OdinDrawer]
public class KeyValuePairDrawer : OdinValueDrawer<KeyValuePair<string, int>>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var value = this.ValueEntry.SmartValue;

        SirenixEditorGUI.BeginBox(label);
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Key", GUILayout.Width(50));
                value = new KeyValuePair<string, int>(
                    GUILayout.TextField(value.Key),
                    EditorGUILayout.IntField(value.Value)
                );
            }
            GUILayout.EndHorizontal();
        }
        SirenixEditorGUI.EndBox();

        this.ValueEntry.SmartValue = value;
    }
}