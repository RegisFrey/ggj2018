﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorizable : MonoBehaviour, IColorizable {
  
  [SerializeField]
  private List<Text> uiText;
  [SerializeField]
  private List<Text> uiTextPop;
  [SerializeField]
  private List<Image> uiFg;
  [SerializeField]
  private List<Image> uiFgPop;
  [SerializeField]
  private List<Image> uiBkg;
  
  public void Colorize(StyleSet styles)
  {
      Colorize(styles.primary);
  }
  
  public void Colorize(Style uiStyle)
  {
    foreach (Text t in uiText)
    {
        t.color = uiStyle.fgColor;
    }
    foreach (Text t in uiTextPop)
    {
        t.color = uiStyle.fgPopColor;
    }
    foreach (Image i in uiFg)
    {
        i.color = uiStyle.fgColor;
    }
    foreach (Image i in uiFgPop)
    {
        i.color = uiStyle.fgPopColor;
    }
    foreach (Image i in uiBkg)
    {
        i.color = uiStyle.bkgColor;
    }
  }
}
