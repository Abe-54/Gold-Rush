using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 5;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().pickupSoundFX.Play();
            Score.Instance.AddToScore(value);
            gameObject.SetActive(false);
        }
    }
}
