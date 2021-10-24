using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isPaused = false;
//    [SerializeField] private GameObject pauseMenuUI;

    public GameObject spawn;
    public GameObject player;
    public GameObject minotaur;
    public Text timerText;
    public FollowPlayer enemy;
    public GameObject EndPanel; 
    public AudioSource cameraAudioSource;

    private bool isSoundOn = true;
    private float startTime;
    private float currentTimer;
    private bool isPlaying = false;

    [SerializeField] private GameObject soundTextOn;
    [SerializeField] private GameObject soundTextOff;

    void Start()
    {
    }

    void Update()
    {
        if (isPlaying == true)
        {
            timerText.text = GetCurrentTimer();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        print("oui");

        if (isPaused)
            ActivateMenu();
        else
            DeactivateMenu();
    }

    private void ActivateMenu()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        AudioListener.pause = true;
//        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        //Cursor.visible = false;
        AudioListener.pause = false;
//        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                             Application.Quit();
        #endif
    }

    public void MenuActions(Collider menuButton)
    {
        if (menuButton.gameObject.name == "MenuQuit")
        {
            QuitGame();
        }
        else if (menuButton.gameObject.name == "MenuSound")
        {
            if (isSoundOn == true)
            {
                soundTextOn.SetActive(false);
                soundTextOff.SetActive(true);
                cameraAudioSource.Stop();
                isSoundOn = false;

            } else if (isSoundOn == false)
            {
                soundTextOn.SetActive(true);
                soundTextOff.SetActive(false);
                cameraAudioSource.Play();
                isSoundOn = true;
            }
        }
        else if (menuButton.gameObject.name == "MenuPlay")
        {
            if (isPlaying == false)
            {
                startTime = Time.time;
                enemy.setPlayerState(true);
            }
            isPlaying = true;
        }
        else if (menuButton.gameObject.name == "EndPoint")
        {
            isPlaying = false;
            EndPanel.SetActive(true);

            timerText.text = "";

            enemy.setPlayerState(false);

            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public string GetCurrentTimer()
    {
        currentTimer = Time.time - startTime;

        string min = ((int)currentTimer / 60).ToString();
        string sec = (currentTimer % 60).ToString("f2");

        return min + ":" + sec;
    }

    public void Restart()
    {
        EndPanel.SetActive(false);

        minotaur.transform.position = new Vector3(170, 301, 163);
        HealthManager healthmanager = player.GetComponent<HealthManager>();
        healthmanager.ChangeSpawnPoint(spawn.transform);
        healthmanager.ReSpawn();

        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
