using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
  
  [SerializeField]
  private Style uiStyle;
  public float UIStyle
  {
    get { return uiStyle; }
    set 
    {
      uiStyle = value;
      ColorizeUI(uiStyle);
    }
  }
  [SerializeField]
  private List<Text> uiText;
  [SerializeField]
  private List<Sprite> uiFg;
  [SerializeField]
  private List<Sprite> uiBkg;

  public void Awake()
  {
      GameManager.Instance.UIManger = this;
  }
  
  public void Start() 
  {
    ColorizeUI(uiStyle);
  }
  
  public void ColorizeUI(Style s)
  {
    foreach (Text t in uiText)
    {
        t.color = s.fgColor;
    }
    foreach (Sprite i in uiFg)
    {
        i.color = s.fgColor;
    }
    foreach (Sprite i in uiBkg)
    {
        i.color = s.bkgColor;
    }
  }
}
