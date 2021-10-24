using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private CharacterController player;
    public FollowPlayer minotaur;
    public Canvas _deathScreen;
    private PlayerSounds _sounds;


    private void Start()
    {
        ReSpawn();
        _sounds = GetComponent<PlayerSounds>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Die();
    }

    public void ChangeSpawnPoint(Transform newPoint)
    {
        spawnPoint = newPoint;
    }

    public void ReSpawn()
    {
        minotaur.Respawn();
        player.enabled = false;
        transform.position = spawnPoint.position;
        player.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoints"))
        {
            Debug.Log("Changing Spawn");
            ChangeSpawnPoint(other.gameObject.transform);
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        _sounds.SetDeathSound();
        _sounds.Play();
        _deathScreen.gameObject.SetActive(true);
    }
}
