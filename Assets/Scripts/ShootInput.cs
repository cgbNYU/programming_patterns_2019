using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : MonoBehaviour
{
    
    //Script hookups
    public Shoot shootScript;
    
    // Start is called before the first frame update
    void Start()
    {
        shootScript = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        ShotCheck();
    }

    public void ShotCheck()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shootScript.Shot(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shootScript.Shot(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            shootScript.Shot(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            shootScript.Shot(Vector3.right);
        }
    }
    
}
