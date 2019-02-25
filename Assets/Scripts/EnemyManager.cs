﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

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
    void Start()
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
        var whichEnemy = (Enemies) Random.Range(0, 2);

        if (whichEnemy == Enemies.Exploder)
        {
            GameObject enemy = Instantiate(Exploder, spawnLoc.transform.position, spawnLoc.transform.rotation);
            enemies.Add(enemy);
        }
        else if (whichEnemy == Enemies.Follower)
        {
            GameObject enemy = Instantiate(Follower, spawnLoc.transform.position, spawnLoc.transform.rotation);
            enemies.Add(enemy);
        }
        else if (whichEnemy == Enemies.Shooter)
        {
            GameObject enemy = Instantiate(Shooter, spawnLoc.transform.position, spawnLoc.transform.rotation);
            enemies.Add(enemy);
        }
    }
    
    //Generate relevant enemies
    private void EnemySpawn(int enemyNum)
    {
        Debug.Log("enemyNum = " + enemyNum);
        for (int i = 0; i <= enemyNum; i++)
        {
            GameObject whichSpawn = spawners[i];
            ChooseEnemyType(whichSpawn);
        }
    }
    
    //Call this method to destroy any enemies flagged for destruction and count down enemy total
    public void EnemyDestroy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
        Debug.Log("Enemy destroyed!");

        if (enemies.Count <= 0)
        {
            enemies.Clear();
            NewWave();
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