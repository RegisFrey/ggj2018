using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  
    public UIManager UIManager;

    [Range(0, 1)]
    public float percentageCorruption = 1f;

    private float percentageCorruptionFirst;
    private float percentageCorruptionSecond;

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
        DontDestroyOnLoad(gameObject);
    }

    public void SetPercentageCorruptions(float first, float second)
    {
        percentageCorruptionFirst = first;
        percentageCorruptionSecond = second;

        // Integration for now:
        percentageCorruption = (percentageCorruptionFirst + percentageCorruptionSecond) / 2f;
    }
    
    public float PercentageCorruption
    {
        get { return percentageCorruption; }
        set { percentageCorruption = value; }
    }
}
