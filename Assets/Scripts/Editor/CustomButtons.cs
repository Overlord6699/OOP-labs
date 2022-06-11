using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomButtons : EditorWindow
{
    public delegate void ClearSmth();
    public event ClearSmth OnClearSmth;
     

    void OnGUI()
    {
        if (GUILayout.Button("Clear", GUILayout.Width(50),
            GUILayout.Height(20)))
        {
            OnClearSmth?.Invoke();
        }
    }
}
