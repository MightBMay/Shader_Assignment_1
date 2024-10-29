using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip LightOn, Correct, Incorrect;
    AudioSource source;
    private void Awake()
    {
        if(instance!=null)Destroy(instance);    
        instance = this;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void PitchShift()
    {
        source.pitch = 1 + Random.Range(-0.15f, 0.2f);
    }
    public void PlayLightSound()
    {
        PitchShift();
        source.PlayOneShot(LightOn);
    }
}
