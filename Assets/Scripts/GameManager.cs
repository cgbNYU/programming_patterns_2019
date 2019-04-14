using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    //Singleton
    public static GameManager Instance = null;
    
    //State machine
    private StateMachine<GameManager> stateMachine;
    
    //UI
    public Text GameManagerText;

    public void Init()
    {
        //Services.EventManager = new EventManager();
        Services.EnemyManager = new EnemyManager();
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        //Set up the singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //Initialize state machine
        stateMachine = new StateMachine<GameManager>(this);
        EventManager.Instance.AddHandler<Events.WaveEnd>(OnWaveEnd);
        EventManager.Instance.AddHandler<Events.PlayerDied>(OnPlayerDied);
        stateMachine.TransitionTo<GameStart>();
        
        //Initialize UI
        GameManagerText.text = "";
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<Events.WaveEnd>(OnWaveEnd);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update(); //Run the current state update function
        Debug.Log(stateMachine.CurrentState);
    }
    
    private void OnWaveEnd(Events.WaveEnd evt)
    {
        stateMachine.TransitionTo<WaveEnd>();
    }

    private void OnPlayerDied(PlayerDied evt)
    {
        stateMachine.TransitionTo<GameOver>();
    }

    private class GameState : StateMachine<GameManager>.State
    {
        protected float gameTimer; //for timing between states
    }

    //Start the game
    private class GameStart : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game Start!");
            if (GameObject.Find("Player") == null)
            {
                Debug.Log("No Player!");
            }
        }

        public override void Update()
        {
            base.Update();
            Parent.TransitionTo<RoundCountdown>();
        }
    }
    
    //Countdown to the wavestart
    private class RoundCountdown : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Round Countdown!");
            gameTimer = 3;
        }

        public override void Update()
        {
            base.Update();
            Instance.GameManagerText.text = gameTimer.ToString();
            gameTimer -= Time.deltaTime;
            
            if (gameTimer <= 0)
            {
                Parent.TransitionTo<WaveStart>();
            }
        }
    }
    
    //Starts the wave and then moves on to the wave itself
    private class WaveStart : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            EventManager.Instance.Fire(new Events.WaveStart());
            Instance.GameManagerText.text = "";
        }

        public override void Update()
        {
            base.Update();
            Parent.TransitionTo<Wave>();
        }
    }
    
    //The wave itself
    private class Wave : GameState
    {
        
    }
    
    //End the wave
    private class WaveEnd : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            gameTimer = 2;
            Instance.GameManagerText.text = "Nice!";
        }

        public override void Update()
        {
            base.Update();
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                Instance.GameManagerText.text = "";
                Parent.TransitionTo<RoundCountdown>();
            }
        }
    }
    
    //Game over state
    private class GameOver : GameState
    {
        
    }
}
