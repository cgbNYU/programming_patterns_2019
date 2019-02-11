using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerShot : MonoBehaviour
{
    
    //Script Hookups
    public Target targetScript;
    public Shoot shootScript;
    
    //Public
    public float shotDelay;
    
    //Private
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        targetScript = GetComponent<Target>();
        shootScript = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        ShotCountdown();
    }

    public void ShotCountdown()
    {
        timer += Time.deltaTime;

        if (timer > shotDelay)
        {
            shootScript.Shot(targetScript.targetDif);
            timer = 0;
        }
    }
}
