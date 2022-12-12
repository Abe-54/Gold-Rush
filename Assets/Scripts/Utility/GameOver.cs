using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text scoreTxt;

    void Start()
    {
        scoreTxt.text = "Final Score:" + Score.Instance.score;
    }

    public void MainMenu()
    {
        DungeonManager.Instance.gameOver = false;
        SceneManager.LoadScene("MainGame");
    }
}
