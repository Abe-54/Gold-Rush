using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ColoredTile : MonoBehaviour
{
    [SerializeField] private string tileColor;

    private Light2D coloredLight;
    
    public Action<string, Light2D> EnteredColoredTile;
    public Action<string, Light2D> ExitedColoredTile;
    
    // Start is called before the first frame update
    void Start()
    {
        coloredLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            EnteredColoredTile?.Invoke(tileColor, coloredLight);
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            ExitedColoredTile?.Invoke(tileColor, coloredLight);
    }
}
