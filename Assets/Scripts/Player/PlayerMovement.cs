using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager manager;
    private CharacterController controller;
    private HealthManager healthmanager;
    private PlayerSounds _playerSounds;
    private AudioSource _source;
    public AudioManager _am;
    public float _baseSpeed = 3f;
    public float _sprint = 1.2f;
    private float _VerticalVelocity = 0;
    public float _gravity = 14f;
    public float jumpForce = 10f;
    private int _time = 0;
    [SerializeField]
    private float _maxStamina = 100;
    [SerializeField]
    private float _stamina;
    private bool _isRunning = false;


    private void Awake()
    {
        healthmanager = GetComponent<HealthManager>();
        controller = GetComponent<CharacterController>();
        _playerSounds = GetComponent<PlayerSounds>();
        _source = GetComponent<AudioSource>();
        _stamina = _maxStamina;
    }

    void Update()
    {
        float speed = HandleSprint();


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (controller.isGrounded)
        {
            FootSteps();
            _VerticalVelocity = -_gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
                _VerticalVelocity = jumpForce;
        }
        else
        {
            _VerticalVelocity -= _gravity * Time.deltaTime;
        }
        Vector3 move = Vector3.zero;
        move = transform.right * x * speed + transform.forward * y * speed + transform.up * _VerticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private float HandleSprint()
    {
        float speed = _baseSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            _isRunning = true;
        else
            _isRunning = false;
        if (_isRunning)
        {
            speed = _baseSpeed * _sprint;
            _stamina -= 10f * Time.deltaTime;
            if (_stamina < 0)
            {
                _isRunning = false;
                _stamina = 0;
                speed = _baseSpeed;
            }
        }
        else if (_stamina < _maxStamina)
        {
            _stamina += 10f * Time.deltaTime;
        }
        return speed;
    }

    private void FootSteps()
    {
        if (_time > 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            _time -= 1;
            return;
        }
        if (!_playerSounds.isPlaying() && controller.velocity.magnitude > 2f)
        {
            _source.volume = Random.Range(0.1f, 0.3f);
            _source.pitch = Random.Range(0.8f, 1.2f);
            _playerSounds.Play();
            _time = _isRunning ? 30 : 45;
            print(_time);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Traps"))
        {
            healthmanager.ReSpawn();
            healthmanager.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if (other.gameObject.CompareTag("MenuButtons"))
        {
            Debug.Log("Menu trigger");
            manager.MenuActions(other);
        }
    }
}
