using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Events;

public class MultiplierManager : MonoBehaviour
{
    //Timer stuff
    private float timer = 0;
    public float resetTime;
    
    //Multiplier numbers
    public int maxMulti;
    private int multiplier;
    
    //Text stuff
    private Text multiText;
    
    //Multiplier bool
    private bool waveRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        multiplier = 1;
        multiText = GetComponent<Text>();
        multiText.text = "X" + multiplier;
        EventManager.Instance.AddHandler<ScoreChanged>(OnScoreChanged);
        EventManager.Instance.AddHandler<WaveEnd>(OnWaveEnd);
        EventManager.Instance.AddHandler<WaveStart>(OnWaveStart);
        EventManager.Instance.AddHandler<PlayerDied>(OnPlayerDied);
        waveRunning = false;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<ScoreChanged>(OnScoreChanged);
        EventManager.Instance.RemoveHandler<PlayerDied>(OnPlayerDied);
        EventManager.Instance.RemoveHandler<WaveEnd>(OnWaveEnd);
        EventManager.Instance.RemoveHandler<WaveStart>(OnWaveStart);
    }

    private void OnWaveEnd(WaveEnd evt)
    {
        waveRunning = false;
    }

    private void OnWaveStart(WaveStart evt)
    {
        waveRunning = true;
    }

    private void OnScoreChanged(ScoreChanged evt)
    {
        Debug.Log("Score changed for Multi");
        if (multiplier < maxMulti)
        {
            multiplier += 1;
            multiText.text = "X" + multiplier;
            EventManager.Instance.Fire(new MultiplierChanged(multiplier));
        }

        timer = resetTime;
        Debug.Log("Timer = " + timer);
    }

    private void OnPlayerDied(PlayerDied evt)
    {
        timer = 0; //reset timer, and thus the multiplier
    }

    // Update is called once per frame
    void Update()
    {
        if (waveRunning)
        {
            MultiplierTimeOut();
        }
    }

    private void MultiplierTimeOut()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            multiplier = 1;
            multiText.text = "X" + multiplier;
            EventManager.Instance.Fire(new MultiplierChanged(multiplier));
        }
    }
}
