using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTarget : Target
{
    public float rotateSpeed = 5f;
    public float radius = 0.1f;
 
    private Vector2 center;
    private float angle;

    private int randomCircleOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;

        randomCircleOffset = Random.Range(-100, 100);
    }
    
    public override void Movement()
    {
        angle += rotateSpeed * Time.deltaTime;
 
        var Circle = new Vector2(Mathf.Sin(angle + randomCircleOffset), Mathf.Cos(angle + randomCircleOffset)) * radius;
        transform.position = center + Circle;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center, radius);
        if (!Application.isPlaying)
        {
            center = transform.position;
        }
    }
}
