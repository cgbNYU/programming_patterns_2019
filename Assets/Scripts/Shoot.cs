using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    //Public variables
    public float shotForce;
    public GameObject bullet;

    public void Shot(Vector3 target)
    {
        GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
        shot.GetComponent<Rigidbody2D>().AddForce(target * shotForce);
    }
}
