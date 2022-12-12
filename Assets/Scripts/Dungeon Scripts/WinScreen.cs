using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject[] winScreenItems;

    public Action<bool> LoadingNewScene;
    
    void Start()
    {
        Timer.Instance.startTimer = false;

        Score.Instance.score += Timer.Instance.timer;
    }

    public void CloseWinScreen()
    {
        foreach (var gameObject in winScreenItems)
        {
            gameObject.SetActive(false);
        }
        
        FindObjectOfType<PlayerController>().CanMove(true);
    }

    public void MainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        LoadingNewScene?.Invoke(false);
        PlayerPrefs.SetInt("HighScore", Score.Instance.score);
        yield return StartCoroutine(Fade.Instance.FadeOut());
        SceneManager.LoadScene("MainGame");
    }
}
