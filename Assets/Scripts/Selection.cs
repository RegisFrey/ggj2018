using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    private Image symbol;
    private bool isCorrect;

    public bool IsCorrect
    {
        get { return isCorrect; }
        set { isCorrect = value; }
    }

    public Image Symbol
    {
        get { return symbol; }
    }

    private void Awake()
    {
        symbol = GetComponent<Image>();
    }
}
