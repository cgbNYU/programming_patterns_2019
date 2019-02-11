using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    
    //Script hookups
    public Target targetScript;
    public Movement moveScript;
    
    // Start is called before the first frame update
    void Start()
    {
        targetScript = GetComponent<Target>();
        moveScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget(targetScript.targetDif.normalized);
    }

    public void FollowTarget(Vector3 target)
    {
        targetScript.FindTarget();
        
        moveScript.Move(target);
    }
}
