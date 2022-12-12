using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("PlayerWon", 1);
            col.GetComponent<PlayerController>().crown.SetActive(true);
            col.GetComponent<PlayerController>().pickupSoundFX.Play();
            Destroy(gameObject);
        }
    }
}
