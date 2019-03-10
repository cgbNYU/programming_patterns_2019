using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Events;

public class HighScoreManager : MonoBehaviour
{
    //Text
    private Text highScoreText;
    
    //score int
    private int highScore;
    
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText = GetComponent<Text>();
        highScoreText.text = "High Score: " + highScore;
        EventManager.Instance.AddHandler<ScoreChanged>(OnScoreChanged);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<ScoreChanged>(OnScoreChanged);
    }

    private void OnScoreChanged(ScoreChanged evt)
    {
        if (evt.NewScore > highScore)
        {
            highScore = evt.NewScore;
            highScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
