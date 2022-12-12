using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score Instance;
    
    public TMP_Text scoreText;
    public int score;

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
        if (PlayerPrefs.HasKey("Score") && SceneManager.GetActiveScene().name.CompareTo("MainGame") != 0)
        {
            score = PlayerPrefs.GetInt("Score");
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddToScore(int num)
    {
        score += num;
    }
}
