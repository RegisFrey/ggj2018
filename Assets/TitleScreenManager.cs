using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour {

    public GameObject titleScreen;

    private void Awake()
    {
        titleScreen.SetActive(true);
    }
    void Start()
    {
        StartCoroutine(GameManager.Instance.CycleUIColors());
    }

    void OnEnable()
    {
        EventsManager.StartListening("HideTitle", HideTitle);
    }

    void OnDisable()
    {
        EventsManager.StopListening("HideTitle", HideTitle);
    }

    void HideTitle()
    {
        titleScreen.SetActive(false);
        StopCoroutine(GameManager.Instance.CycleUIColors());
        LoadLevelManager.Instance.LoadLevel();
        SoundManager.Instance.StartGameMusic();
    }
}
