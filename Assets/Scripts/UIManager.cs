using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  
  public Camera mainCamera;
  
  [SerializeField]
  private StyleSet uiStyleSet;
  public StyleSet UIStyleSet
  {
    get { return uiStyleSet; }
    set 
    {
      uiStyleSet = value;
      //uiStyle = uiStyleSet.primary;
      ColorizeUI();
    }
  }
  /*
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
  */
  [SerializeField]
  private List<Text> uiText;
  [SerializeField]
  private List<Image> uiFg;
  [SerializeField]
  private List<Image> uiBkg;
  [SerializeField] // can't due to Interface
  private IList<IColorizable> uiColorizable;

  public void Awake()
  {
    GameManager.Instance.UIManager = this;
  }
  
  public void Start() 
  {
      GatherColorizable();
      ColorizeUI();
  }
  
  public void GatherColorizable()
  {
      uiColorizable = InterfaceHelper.FindObjects<IColorizable>();
  }
  
  public void ColorizeUI(StyleSet s)
  {
      uiStyleSet = s;
      ColorizeUI();
  }
  
  public void ColorizeUI()
  {
    //uiStyle = uiStyleSet.primary;
    if(uiColorizable == null)
    {
        GatherColorizable();
    }
    
    mainCamera.backgroundColor = uiStyleSet.primary.bkgColor;
    
    foreach (Text t in uiText)
    {
        t.color = uiStyleSet.primary.fgColor;
    }
    foreach (Image i in uiFg)
    {
        i.color = uiStyleSet.primary.fgColor;
    }
    foreach (Image i in uiBkg)
    {
        i.color = uiStyleSet.primary.bkgColor;
    }
    foreach (IColorizable c in uiColorizable)
    {
        //c.Colorize(uiStyle);
        c.Colorize(uiStyleSet);
    }
  }
}
