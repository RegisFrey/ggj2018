using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Range(0, 1)]
    public float percentageCorruption;


    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }

    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public float PercentageCorruption
    {
        get { return percentageCorruption; }
        set { percentageCorruption = value; }
    }
}
