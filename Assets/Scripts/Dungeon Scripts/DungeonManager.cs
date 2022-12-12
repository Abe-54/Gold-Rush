using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonManager : MonoBehaviour
{
    public List<String> minigameSceneNames;

    public bool gameOver = false;
    public GameObject gameOverScreen;

    public static DungeonManager Instance; 
    
    private LoadNewScene[] loadingAreas;
    private PlayerController player;
    
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
        loadingAreas = FindObjectsOfType<LoadNewScene>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var loadingArea in loadingAreas)
        {
            loadingArea.LoadingNewScene += player.CanMove;
        }
        
        if (Timer.Instance.timer <= 0 && !gameOver)
        {
            gameOver = true;
            Instantiate(gameOverScreen, Vector3.zero, Quaternion.identity);
        }
    }

    public string pickRandomMiniGame()
    {
        return minigameSceneNames[Random.Range(0, minigameSceneNames.Count)];
    }
}
