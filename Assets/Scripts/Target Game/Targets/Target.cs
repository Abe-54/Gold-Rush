using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    public Action<GameObject> hitTarget;
    
    private void Update()
    {
        Movement();
    }

    public abstract void Movement();
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            hitTarget?.Invoke(gameObject);
            Destroy(col.gameObject);
        }
    }
}
