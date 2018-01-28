using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  
  [SerializeField]
  private Style uiStyle;
  public Style UIStyle
  {
    get { return uiStyle; }
    set 
    {
      uiStyle = value;
      ColorizeUI();
    }
  }
  [SerializeField]
  private List<Text> uiText;
  [SerializeField]
  private List<Image> uiFg;
  [SerializeField]
  private List<Image> uiBkg;
  [SerializeField]
  private List<UIColorizable> uiColorizable;

  public void Awake()
  {
    GameManager.Instance.UIManager = this;
  }
  
  public void Start() 
  {
    ColorizeUI();
  }
  
  public void ColorizeUI()
  {
    foreach (Text t in uiText)
    {
        t.color = uiStyle.fgColor;
    }
    foreach (Image i in uiFg)
    {
        i.color = uiStyle.fgColor;
    }
    foreach (Image i in uiBkg)
    {
        i.color = uiStyle.bkgColor;
    }
    foreach (UIColorizable c in uiColorizable)
    {
        c.Colorize(uiStyle);
    }
  }
}
