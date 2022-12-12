using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTarget : Target
{
    public float length = 5f;
    public float movementSpeed = 5f;
 
    private Vector2 center;
    private float position;
    
    private int randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        
        randomOffset = Random.Range(-100, 100);
    }
    public override void Movement()
    {
        position += movementSpeed * Time.deltaTime;
 
        var offset = new Vector2(0, Mathf.Sin(position + randomOffset)) * length;
        transform.position = center + offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(center.x, center.y - (length)), new Vector3(center.x, center.y + (length)));
        if (!Application.isPlaying)
        {
            center = transform.position;
        }
    }
}
