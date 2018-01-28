using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EndResult
{
    SUCCESS,
    FAILURE,
    TIME_UP,
};

public class GameManager : MonoBehaviour {
  
    // Singleton references
    public UIManager UIManager;
    
    // Temporary GO references
    public Text timer;
    
    // Settings
    public float secondsPerLevel = 60f;

    private float levelTimeRemaining = 60f;

    private float targetLeftTrigger, targetRightTrigger;
    
    [Range(0, 1)]
    public float percentageCorruption = 1f;
    public float percentageCorruptionLeft;
    public float percentageCorruptionRight;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }

    }

    void OnEnable()
    {
        EventsManager.StartListening("NewLevelLoaded", NewLevelLoaded);
    }

    void OnDisable()
    {
        EventsManager.StopListening("NewLevelLoaded", NewLevelLoaded);
    }

    void NewLevelLoaded()
    {
        levelTimeRemaining = LoadLevelManager.Instance.GetCurrentLevel().seconds;

        // Set random targets for vibration
        targetLeftTrigger = Random.Range(0f, 1f);
        targetRightTrigger = Random.Range(0f, 1f);
    }

    public void LevelCompleted(EndResult result)
    {
        if (result == EndResult.TIME_UP)
        {
            // GameOver();
        }
        else if (result == EndResult.FAILURE)
        {
            // GameOver();
        }
        else
        {
            LoadLevelManager.Instance.LoadNextLevel();
        }
    }

    public float TargetLeftTrigger { get { return targetLeftTrigger; } }
    public float TargetRightTrigger { get { return targetRightTrigger; } }


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
            timer.text = "00.000";
            LevelCompleted(EndResult.TIME_UP);
       }
    }

    public void SetPercentageCorruptions(float left, float right)
    {
        percentageCorruptionLeft = left;
        percentageCorruptionRight = right;

        // Integration for now:
        percentageCorruption = (percentageCorruptionLeft + percentageCorruptionRight) / 2f;
    }
    
    public float PercentageCorruption
    {
        get { return percentageCorruption; }
        set { percentageCorruption = value; }
    }

    public float GetPercentageCorruption(bool isLeft)
    {
        return isLeft ? percentageCorruptionLeft : percentageCorruptionRight;
    }
}
