using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private SpriteRenderer gun;

    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Current Rotation Z:" + transform.rotation.z);
        
        FlipGun();
    }

    private void FlipGun()
    {
        var dir = new Vector3();
        
        if (!player.facingRight)
        {
            dir = transform.position - crosshair.transform.position;
        }
        else
        {
            dir = crosshair.transform.position - transform.position;
        }
        
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        gun.flipY = (transform.rotation.z > 0.7f || transform.rotation.z < -0.7f);
    }
}
