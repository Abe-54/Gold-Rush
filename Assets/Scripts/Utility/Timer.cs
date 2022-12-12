using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    
    public TMP_Text timerText;
    public int timer = 10;
    public bool startTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Timer") && SceneManager.GetActiveScene().name.CompareTo("MainGame") != 0)
        {
            timer = PlayerPrefs.GetInt("Timer");
            startTimer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Timer:" + timer;
        
        if(startTimer)
            StartCoroutine(timerCoroutine());
    }

    IEnumerator timerCoroutine()
    {
        startTimer = false;
        while (timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
