using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
  
    // Singleton references
    public UIManager UIManager;
    
    // Temporary GO references
    public Text timer;
    
    // Settings
    public float secondsPerLevel = 60f;

    // Runtime
    public float levelTimeRemaining = 60f;
    
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
    
    public void Update() 
    {
       levelTimeRemaining -= Time.deltaTime;
       System.TimeSpan currentTimeRemaining = System.TimeSpan.FromSeconds(levelTimeRemaining);
       //timer.text = currentTimeRemaining.ToString("s\.fff");
       if(timer != null) {
         timer.text = string.Format("{0}.{1}", currentTimeRemaining.Seconds, currentTimeRemaining.Milliseconds);
       }
       if ( levelTimeRemaining < 0 )
       {
           // GameOver();
       }
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
