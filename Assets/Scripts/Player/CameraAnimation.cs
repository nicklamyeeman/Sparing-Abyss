using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animation _anim;
    private CharacterController _player;

    void Start()
    {
        _anim = GetComponent<Animation>();
        _player = GetComponentInParent<CharacterController>();
        print("OK");
    }

    void Update()
    {
        if (_anim)
            AnimateCamera();
    }

    private void AnimateCamera()
    {
        if (!_anim.isPlaying && _player.velocity.magnitude > 2.3f)
        {
            print("Animate Head");
            _anim.Play();
        }
        else if (_player.velocity.magnitude < 2)
        {
            _anim.Stop();
            _anim.Rewind();
        }
    }
}
