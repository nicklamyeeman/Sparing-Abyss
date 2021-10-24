using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip _deathClip;
    [SerializeField]
    private AudioClip _footSteps;
    [SerializeField]
    private AudioClip _breath;
    private AudioSource _sound;

    // Start is called before the first frame update
    void Start()
    {
        _sound = GetComponent<AudioSource>();
    }

    public void Play()
    {
        _sound.Play();
    }

    public void Stop()
    {
        _sound.Stop();
    }

    public void SetDeathSound()
    {
        _sound.clip = _deathClip;
    }

    public void SetFootStepsSound()
    {
        _sound.clip = _footSteps;
    }

    public bool isPlaying()
    {
        return _sound.isPlaying;
    }
}
