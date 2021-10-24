using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _sounds;
    [SerializeField]
    private AudioSource _as;

    public void SetClip(int clip)
    {
        _as.clip = _sounds[clip];
    }

    public void Stop()
    {
        _as.Stop();
    }

    public void Play()
    {
        if (!_as.isPlaying)
            _as.Play();
    }
}
