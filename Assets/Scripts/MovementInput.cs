using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{

    //Script hookups
    private Movement moveScript;
    
    //Private variables
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        //Grab the Movement script
        //If it can't be found, error
        moveScript = GetComponent<Movement>();
        
        //Set main camera
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }

    public void MoveInput()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        if (Input.GetKey(KeyCode.W) && screenPos.y < Screen.height) //up
        {
            moveScript.Move(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.S) && screenPos.y > 0) //down
        {
            moveScript.Move(Vector3.down);
        }

        if (Input.GetKey(KeyCode.A) && screenPos.x > 0) //left
        {
            moveScript.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D) && screenPos.x < Screen.width) //right
        {
            moveScript.Move(Vector3.right);
        }
    }
    
}
