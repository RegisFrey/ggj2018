using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
 public class AudioDistortionClass : System.Object
    {
    public AudioSource src;
    public float defaultVolume=1;
    public float targetVolume=1;
    public float defaultPitch=1;
    public float targetPitch=1;
    public float defaultPan=0;
    public float targetPan=0;

    private float currentPan, currentVolume, currentPitch;

     // val = (default-target)*corruption + target
    public void UpdateValuesWithCorruption(float corruption)
    {
        float vol = (defaultVolume - targetVolume) * corruption + targetVolume;
        float pitch = (defaultPitch - targetPitch) * corruption + targetPitch;
        float panStereo = (defaultPan - targetPan) * corruption + targetPan;
        UpdateValues(vol, pitch, panStereo);
    }

    private void UpdateValues(float volume, float pitch, float panStereo)
    {
        currentPitch = pitch;
        currentVolume = volume;
        currentPan = panStereo;

        src.pitch = currentPitch;
        src.volume = currentVolume;
        src.panStereo = currentPan;
    }

    public void Play()
    {
        src.Play();
    }

}

public class SoundManager : MonoBehaviour {

    [Header("Testing Distortion Values")]
    [Range(0, 1)]
    public float fullDistortionEffect;

    public AudioDistortionClass baseBeat;
    public AudioDistortionClass wupDownUp;
    public AudioDistortionClass shimmer;
    public AudioDistortionClass noise;

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

    public void SetAudioCorruption(float corruption)
    {
        baseBeat.UpdateValuesWithCorruption(corruption);
        wupDownUp.UpdateValuesWithCorruption(corruption);
        shimmer.UpdateValuesWithCorruption(corruption);
        noise.UpdateValuesWithCorruption(corruption);
    }

    public void SetPitch(float pitch)
    {
        for(int i=0;i<audioSources.Length;i++)
        {
            audioSources[i].pitch = pitch;
        }
    }

    public void MakeDistortion(bool shouldDistort)
    {
        distortionFilter.distortionLevel = shouldDistort ? fullDistortionEffect : 0;
    }

    public void StartGameMusic()
    {
        baseBeat.Play();
        wupDownUp.Play();
        shimmer.Play();
        noise.Play();
    }
}
