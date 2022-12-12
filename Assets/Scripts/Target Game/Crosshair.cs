using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 1.0f;
        transform.position = position;
    }
}
