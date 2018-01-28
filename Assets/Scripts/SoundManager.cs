using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [Header("Testing Distortion Values")]
    [Range(0, 1)]
    public float fullDistortionEffect;

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

    public void SetVolume(float volume)
    {
        //TODO: fill this
    }

    public void SetStereoPan(float pan)
    {
        //TODO: fill this
    }

    public void MakeDistortion(bool shouldDistort)
    {
        distortionFilter.distortionLevel = shouldDistort ? fullDistortionEffect : 0;
    }
}
