using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource[] audioSources;

    private static SoundManager _instance;
    public static SoundManager Instance
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

        audioSources = GetComponents<AudioSource>();
    }

    public void SetPitch(float pitch1, float pitch2)
    {
        audioSources[0].pitch = pitch1;
        audioSources[audioSources.Length - 1].pitch = pitch2;
    }
}
