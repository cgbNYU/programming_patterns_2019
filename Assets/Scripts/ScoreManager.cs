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

    private int currentMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>(); //grab the text
        EventManager.Instance.AddHandler<EnemyDied>(OnEnemyDied); //register enemydied handler
        EventManager.Instance.AddHandler<MultiplierChanged>(OnMultiplierChanged); //register MultiplierChanged handler
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<EnemyDied>(OnEnemyDied); //deregister on scoremanager destruction
        EventManager.Instance.RemoveHandler<MultiplierChanged>(OnMultiplierChanged); //deregister MultiplierChanged handler
    }

    private void OnMultiplierChanged(MultiplierChanged evt)
    {
        currentMultiplier = evt.NewMultiplier;
    }

    private void OnEnemyDied(EnemyDied evt)
    {
        Debug.Log("Enemy Died: " + evt.PointValue);
        currentScore += evt.PointValue * currentMultiplier; //update points values

        scoreText.text = "Score: " + currentScore; //set the score text
        
        EventManager.Instance.Fire(new ScoreChanged(currentScore)); //fire scorechange event
    }
}
