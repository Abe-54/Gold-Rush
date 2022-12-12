using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TRLoadingZone : MonoBehaviour
{
    public GameObject finalScoreScreen;
    public TMP_Text finalScoreTxt;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().CanMove(false);
            finalScoreTxt.text = "Final Score:" + Score.Instance.score;
            finalScoreScreen.SetActive(true);
        }
    }
}
