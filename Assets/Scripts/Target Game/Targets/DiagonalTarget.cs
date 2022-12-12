using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalTarget : Target
{
    public float length = 5f;
    public float movementSpeed = 5f;

    public bool isRightDiagonal = true;
 
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

        var offset = new Vector2();
        
        if (isRightDiagonal)
        {
            offset = new Vector2(Mathf.Sin(position + randomOffset), Mathf.Sin(position + randomOffset)) * length;
            transform.position = center + offset;
        }
        else if(!isRightDiagonal)
        {
            offset = new Vector2(-Mathf.Cos(position + randomOffset), Mathf.Cos(position + randomOffset)) * length;
            transform.position = center - offset;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (isRightDiagonal)
        {
            Gizmos.DrawLine(new Vector3(center.x - (length), center.y - (length)), new Vector3(center.x + (length), center.y + (length)));
        }
        else
        {
            Gizmos.DrawLine(new Vector3(center.x + (length), center.y - (length)), new Vector3(center.x - (length), center.y + (length)));
        }
        
        if (!Application.isPlaying)
        {
            center = transform.position;
        }
    }
}
