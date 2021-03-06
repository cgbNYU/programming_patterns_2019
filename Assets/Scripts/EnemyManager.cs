﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;

public class EnemyManager : MonoBehaviour
{

    //Singleton
    public static EnemyManager instance = null;
    //Inspector variables to determine min and max enemies per wave
    public int enemyMin = 3;
    public int enemyMax = 8;
    
    //Array of empty gameobjects that act as spawn locations
    public List<GameObject> defaultSpawners;
    private List<GameObject> spawners;
    
    //Enemy prefabs and enumerator
    public GameObject Follower;
    public GameObject Shooter;
    public GameObject Exploder;

    enum Enemies
    {
        Follower,
        Shooter,
        Exploder
    }
    
    //List of enemies
    private List<GameObject> enemies;
    
    // Start is called before the first frame update
    void Awake()
    {
    }

    private void Start()
    {
        EventManager.Instance.AddHandler<WaveStart>(OnWaveStart);
        EventManager.Instance.AddHandler<EnemyDied>(EnemyDestroy);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<WaveStart>(OnWaveStart);
        EventManager.Instance.RemoveHandler<EnemyDied>(EnemyDestroy);
    }

    private void OnWaveStart(WaveStart evt)
    {
        NewWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Generate a random number of enemies between enemyMin and enemyMax
    private void ChooseEnemyNum()
    {
        EnemySpawn(Random.Range(enemyMin, enemyMax));
    }
    
    //Take a random piece of the 10 for each enemy type
    private void ChooseEnemyType(GameObject spawnLoc)
    {
        var whichEnemy = (Enemies) Random.Range(0, 3); //int range is exclusive
        
        //This should actually be an array than is pulled from with a random range
        if (whichEnemy == Enemies.Exploder)
        {
            GameObject enemy = GameObject.Instantiate(Exploder, spawnLoc.transform.position, spawnLoc.transform.rotation);
            enemies.Add(enemy);
        }
        else if (whichEnemy == Enemies.Follower)
        {
            GameObject enemy = GameObject.Instantiate(Follower, spawnLoc.transform.position, spawnLoc.transform.rotation);
            enemies.Add(enemy);
        }
        else if (whichEnemy == Enemies.Shooter)
        {
            GameObject enemy = GameObject.Instantiate(Shooter, spawnLoc.transform.position, spawnLoc.transform.rotation);
            enemies.Add(enemy);
        }
    }
    
    //Generate relevant enemies
    private void EnemySpawn(int enemyNum)
    {
        //Not sure where to add it, but this should have an interval between enemies so it's not immediate death
        //Maybe they spawn a marker where they are going to spawn before it happens
        Debug.Log("enemyNum = " + enemyNum);
        for (int i = 0; i <= enemyNum; i++)
        {
            GameObject whichSpawn = spawners[i]; //this would be better if it picked randomly and then dropped that spawner from the potential list
            ChooseEnemyType(whichSpawn);
        }
    }
    
    //Call this method to destroy any enemies flagged for destruction and count down enemy total
    public void EnemyDestroy(EnemyDied evt)
    {
        enemies.Remove(evt.Enemy);
        GameObject.Destroy(evt.Enemy);
        Debug.Log("Enemy destroyed!");

        if (enemies.Count <= 0)
        {
            enemies.Clear(); //I think this is redundant because enemies gets set again later, but that's probably fine
            EventManager.Instance.Fire(new Events.WaveEnd());
        }
    }
    
    //When enemy total reaches 0, start a new wave
    private void NewWave()
    {
        spawners = new List<GameObject>(defaultSpawners);
        enemies = new List<GameObject>();
        ChooseEnemyNum();
    }
}
