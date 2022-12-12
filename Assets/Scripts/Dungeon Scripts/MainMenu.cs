using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] mainMenuGameObjects;
    public GameObject[] tutorials;
    public GameObject[] gameplayUI;
    public GameObject player;
    public GameObject[] gameplayObjects;

    public int currentTutorial = -1;

    public void StartGame()
    {
        PlayerPrefs.DeleteKey("Timer");
        PlayerPrefs.DeleteKey("Score");

        currentTutorial++;
        
        foreach (var gameObject in mainMenuGameObjects)
        {
            gameObject.SetActive(false);
        }
        
        tutorials[0].SetActive(true);
    }

    public void Next()
    {
        tutorials[0].SetActive(false);
        tutorials[1].SetActive(true);
        player.SetActive(true);
        player.GetComponent<PlayerController>().CanMove(true);
    }

    void Update()
    {
        if (currentTutorial == 0 && player.transform.position.x != 0 && player.transform.position.y != 0)
        {
            tutorials[1].SetActive(false);
            tutorials[2].SetActive(true);
            foreach (var gameObject in gameplayUI)
            {
                gameObject.SetActive(true);
            }
            
            foreach (var gameObject in gameplayObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
