using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    
    //Public
    public float speed;

    //Move object in Direction
    public void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
