using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    //Public
    public GameObject target;
    public Vector3 targetDif;
    
    // Start is called before the first frame update
    void Start()
    {
      //  target = GameObject.FindWithTag("Player");
    }

    public void FindTarget()
    {
        targetDif = target.transform.position - transform.position;
    }
}
