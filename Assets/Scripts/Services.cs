using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Services
{
    // Event Manager  
   /* private static EventManager _eventManager;
    public static EventManager EventManager   
    {     
        get 
        {            
            return _eventManager;
        }     
        set 
        { 
            _eventManager = value; 
        }   
    }*/
    
    // Enemy Manager
    private static EnemyManager _enemyManager;

    public static EnemyManager EnemyManager
    {
        get
        {   
            return _enemyManager;
        }
        set { _enemyManager = value; }
    }
}
