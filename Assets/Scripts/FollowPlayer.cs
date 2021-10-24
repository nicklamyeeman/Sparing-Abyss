using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private NavMeshAgent nma;
    private Animator animator;
    private HealthManager healthmanager;
    private MinotaurSounds _ms;

    public float hitDistance = 3f;
    public float killDistance = 1f;
    public float walkSpeed = 6f;
    public float runRadius = 10f;
    public float runSpeed = 12f;
    public GameObject target;

    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        healthmanager = target.GetComponent<HealthManager>();
        _ms = GetComponent<MinotaurSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            nma.SetDestination(target.transform.position);

            if (IsPlayerNear(hitDistance) == true)
            {
                animator.SetTrigger("onPlayer");
                _ms.PlayOneTime();
            } else
            {
                _ms.Reset();
            }
            LaunchMinotaurRun();
        }
    }

    public void KillPlayer()
    {
        if (IsPlayerNear(killDistance) == true)
        {
            healthmanager.ReSpawn();
            healthmanager.Die();
        }
    }

    public void Respawn()
    {
        isPlaying = false;
        transform.position = new Vector3(170, 301, 163);
    }

    public void setPlayerState(bool state) { isPlaying = state; }

    private void LaunchMinotaurRun()
    {
        if (IsPlayerNear(runRadius) == false)
        {
            nma.speed = runSpeed;
            animator.SetBool("isRunning", true);
        }
        else
        {
            nma.speed = walkSpeed;
            animator.SetBool("isRunning", false);
        }
    }

    private bool IsPlayerNear(float distance)
    {
        if ((transform.position.x <= target.transform.position.x + distance && transform.position.x >= target.transform.position.x - distance) &&
        transform.position.z <= target.transform.position.z + distance && transform.position.z >= target.transform.position.z - distance)
        {
            return true;
        }
        return false;
    }
}
