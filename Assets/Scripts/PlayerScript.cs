using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

     //Public Variables
    public float moveSpeed;
    public float shotForce;
    public GameObject bullet;

    //Private variables
    private Camera cam;
    private Vector3 playerScale;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Screen Height: " + Screen.height);
        Debug.Log("Screen Width: " + Screen.width);

        cam = Camera.main;

        float playerHeight = transform.localScale.y / 2;
        float playerWidth = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        ShotCheck();
    }

    public void PlayerMove()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        if (Input.GetKey(KeyCode.W)) //up
        {
            Debug.Log("Player height: " + screenPos.y);
            if (screenPos.y < Screen.height)
            {
                transform.position += moveSpeed * Vector3.up * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.S)) //down
        {
            Debug.Log("Player height: " + screenPos.y);
            if (screenPos.y > 0)
            {
                transform.position += moveSpeed * Vector3.down * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.A)) //left
        {
            if (screenPos.x > 0)
            {
                transform.position += moveSpeed * Vector3.left * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.D)) //right
        {
            if (screenPos.x < Screen.width)
            {
                transform.position += moveSpeed * Vector3.right * Time.deltaTime;
            }
        }
    }

    public void ShotCheck()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.up * shotForce);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.down * shotForce);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotForce);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotForce);
        }
    }
}
