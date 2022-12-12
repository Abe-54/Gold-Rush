using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadNewScene : MonoBehaviour
{
    [SerializeField] private string minigameScene;

    public bool isRandomLoadingZone = false;

    public Action<bool> LoadingNewScene;
    
    // Start is called before the first frame update
    void Start()
    {
        if(isRandomLoadingZone)
            minigameScene = DungeonManager.Instance.pickRandomMiniGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRandomLoadingZone)
        {
            if(minigameScene.CompareTo(SceneManager.GetActiveScene().name) == 0)
                minigameScene = DungeonManager.Instance.pickRandomMiniGame();
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(TransitionToNewScene());
        }
    }

    IEnumerator TransitionToNewScene()
    {
        LoadingNewScene?.Invoke(false);
        PlayerPrefs.SetInt("Timer", Timer.Instance.timer);
        
        if(SceneManager.GetActiveScene().name.CompareTo("MainGame") != 0)
            Score.Instance.AddToScore(10);
        
        PlayerPrefs.SetInt("Score", Score.Instance.score);
        yield return StartCoroutine(Fade.Instance.FadeOut());
        SceneManager.LoadScene(minigameScene);
    }
}
