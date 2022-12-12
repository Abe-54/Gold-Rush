using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTarget : Target
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
 
        var offset = new Vector2(Mathf.Sin(position + randomOffset), 0) * length;
        transform.position = center + offset;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(center.x - (length), center.y), new Vector3(center.x + (length), center.y));
        if (!Application.isPlaying)
        {
            center = transform.position;
        }
    }
}
