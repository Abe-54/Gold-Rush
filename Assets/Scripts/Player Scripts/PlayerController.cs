using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool facingRight;

    public GameObject crown;

    public AudioSource pickupSoundFX;
    
    private Rigidbody2D rb2d;
    private float horizontal = 0, vertical = 0;
    
    [SerializeField] private bool canPlayerMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        facingRight = true;

        if (!PlayerPrefs.HasKey("PlayerWon")) return;
        
        if(PlayerPrefs.GetInt("PlayerWon") == 1)
            crown.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(DungeonManager.Instance != null)
            if (DungeonManager.Instance.gameOver) return;
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0 && !facingRight)
        {
            FlipPlayer();
        }
        else if(horizontal < 0 && facingRight)
        {
            FlipPlayer();
        }
    }

    void FixedUpdate()
    {
        if(canPlayerMove)
            rb2d.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;
    }

    void FlipPlayer()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void CanMove(bool canMove)
    {
        canPlayerMove = canMove;
    }
}
