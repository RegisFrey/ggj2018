using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelManager : MonoBehaviour {

    public Level[] levels;

    private int curLevelIndex=0;
    private Level currentLevel;
   
    private void LoadLevel(int levelIndex=0)
    {
        currentLevel = levels[levelIndex];
        EventsManager.TriggerEvent("NewLevelLoaded");
    }

    public void LoadNextLevel()
    {
        curLevelIndex++;
        if(curLevelIndex==levels.Length)
        {
            curLevelIndex = 0;
        }
        LoadLevel(curLevelIndex);
    }

    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    private static LoadLevelManager _instance;
    public static LoadLevelManager Instance
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

    private void Start()
    {
        LoadLevel();
    }
}
