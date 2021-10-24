using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurSounds : MonoBehaviour
{
    private AudioManager _am;

    private bool _played = false;
    // Start is called before the first frame update
    void Start()
    {
        _am = GetComponent<AudioManager>();
        if (_am)
        {
            _am.SetClip(0);
            _am.Play();
        }
    }

    public void PlayOneTime()
    {
        if (_played)
            return;
        _played = true;
        _am.SetClip(0);
        _am.Play();
    }

    public void Reset()
    {
        _played = false;
    }
}
