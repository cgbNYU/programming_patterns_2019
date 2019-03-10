using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Events;

//ScoreManager will read enemy destroyed events and update the score accordingly
//It will also fire a ScoreChanged event to the HighScoreManager and the MultiplierManager
public class ScoreManager : MonoBehaviour
{
    private Text scoreText;

    private int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>(); //grab the text
        EventManager.Instance.AddHandler<EnemyDied>(OnEnemyDied); //register enemydied handler
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<EnemyDied>(OnEnemyDied); //deregister on scoremanager destruction
    }

    private void OnEnemyDied(EnemyDied evt)
    {
        Debug.Log("Enemy Died: " + evt.PointValue);
        currentScore += evt.PointValue; //update points values

        scoreText.text = "Score: " + currentScore; //set the score text
    }
}
