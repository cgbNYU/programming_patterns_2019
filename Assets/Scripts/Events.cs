using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    //Called when the score changes
    public class ScoreChanged : GameEvent
    {
        public int NewScore { get; }
    
        public ScoreChanged(int newScore)
        {
            NewScore = newScore;
        }
    }

    //Called when player dies
    public class PlayerDied : GameEvent {}

    //Called when an enemy dies
    public class EnemyDied : GameEvent
    {
        public int PointValue { get; }
    
        public EnemyDied(int value)
        {
            PointValue = value;
        }
    }
    
    //Called when the Score Multiplier changes
    public class MultiplierChanged : GameEvent
    {
        public int NewMultiplier { get; }

        public MultiplierChanged(int newMulti)
        {
            NewMultiplier = newMulti;
        }
    }
}
