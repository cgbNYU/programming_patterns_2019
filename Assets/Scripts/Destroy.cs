using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;

public class Destroy : MonoBehaviour
{
    
    //Public
    public string tagName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
            EventManager.Instance.Fire(new PlayerDied());
            Destroy(gameObject);
        }
    }
}
