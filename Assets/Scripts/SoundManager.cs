using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource[] audioSources;
    private AudioDistortionFilter distortionFilter;

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
        distortionFilter = GetComponent<AudioDistortionFilter>();
    }

    public void SetPitch(float pitch)
    {
        for(int i=0;i<audioSources.Length;i++)
        {
            audioSources[i].pitch = pitch;
        }
    }

    public void SetDistortion(float distortionLevel)
    {
        distortionFilter.distortionLevel = distortionLevel;

    }
}
