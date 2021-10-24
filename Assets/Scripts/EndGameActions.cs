using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndGameActions : MonoBehaviour
{
    [SerializeField] private Text endTimerText;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        endTimerText.text = manager.GetCurrentTimer();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
