using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;

public class EnemyDestroy : MonoBehaviour
{
    //Public
    public string tagName;
    public int pointValue;
    
    //Private
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        //Registers Handler to check for player death
        EventManager.Instance.AddHandler<PlayerDied>(OnPlayerDied);
    }
    
    //When destroyed, remove handlers
    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<PlayerDied>(OnPlayerDied);
    }
    
    //Destroy enemies on player death
    private void OnPlayerDied(PlayerDied evt)
    {
        Destroy(gameObject);
    }

    private void OnEnemyDestroyed(EnemyDestroy evt)
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
            EventManager.Instance.Fire(new EnemyDied(pointValue, gameObject)); //when an enemy is destroyed by a bullet, grant points
        }
    }
}
