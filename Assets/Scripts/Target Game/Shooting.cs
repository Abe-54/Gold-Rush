using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shootingPos;
    public GameObject gun;
    
    public float fireForce;

    [SerializeField] private AudioSource shootSoundFX;
    
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        shootSoundFX.Play();
        
        GameObject bullet = Instantiate(bulletPrefab, shootingPos.transform.position, gun.transform.rotation);
        
        Rigidbody2D bulletRb =  bullet.GetComponent<Rigidbody2D>();

        Vector3 bulletDir = new Vector3();

        if (!player.facingRight)
        {
            bulletDir = -shootingPos.transform.right;
            bullet.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            bulletDir = shootingPos.transform.right;
            bullet.GetComponent<SpriteRenderer>().flipX = false;
        }

        bulletRb.AddForce(bulletDir * fireForce, ForceMode2D.Impulse);
    }
    
    void FlipBullet()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}
