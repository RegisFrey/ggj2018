using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Editor;

[CustomEditor(typeof(UIManager))]
public class UIManagerInspector : Editor {
  public override void OnInspectorGUI () 
  {
    if (GUILayout.Button("Colorize UI"))
    {
      if(target.GetType() == typeof(UseGetterSetter))
      {
        UIManger uiManager = (UIManger)target;
        uiManager.ColorizeUI();
      }
    }
    // draw normal controls
    base.OnInspectorGUI();
  }
}
