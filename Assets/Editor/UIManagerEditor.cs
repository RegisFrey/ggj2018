using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIManager))]
public class UIManagerInspector : Editor {
  public override void OnInspectorGUI () 
  {
    if (GUILayout.Button("Colorize UI"))
    {
      if(target.GetType() == typeof(UIManager))
      {
        UIManager uiManager = (UIManager)target;
        uiManager.ColorizeUI();
      }
    }
    // draw normal controls
    base.OnInspectorGUI();
  }
}
