using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    float currentGameTime;

    int blueScore;
    int redScore;

    public State CurrentState { get; private set; }
    public State PendingState { get; private set; }

    Dictionary<Type, State> stateCache = new Dictionary<Type, State>();

    public void Initialize()
    {
        Services.eventManager.RegisterListener<GoalScored>(UpdateScore);
        Services.eventManager.RegisterListener<StartGame>(OnStartGame);
        Services.eventManager.RegisterListener<GameOver>(OnGameOver);
        CurrentState = TransitionState<StateMenu>();
    }

    void OnStartGame(SGEvent e)
    {
        ResetGameTime();
        Services.gameManager.ShowScorePanel();
        redScore = 0;
        blueScore = 0;
        SetScoreText(redScore, blueScore);
        PendingState = TransitionState<StateGameInProgress>();
    }

    void OnGameOver(SGEvent e)
    {
        Services.gameManager.ShowGameOverPanel();
    }

    public void Update()
    {
        CheckPendingTransition();
        CurrentState.Update();
    }

    void CheckPendingTransition()
    {
        if (PendingState == null) return;

        CurrentState?.OnExit();
        CurrentState = PendingState;
        CurrentState.OnEnter();

        PendingState = null;
    }

    public void ResetGameTime()
    {
        currentGameTime = 0.0f;
    }

    public void UpdateGameTime(float deltaTime)
    {
        currentGameTime += deltaTime;

        Services.gameManager.timerText.text = currentGameTime.ToString("#.##");

        if(currentGameTime >= Services.gameManager.defaultTimeLimit)
        {
            PendingState = TransitionState<StateGameOver>();
        }
    }

    public TState TransitionState<TState>() where TState : State
    {
        // check if we already created instance of newState
        var type = typeof(TState);
        if (stateCache.TryGetValue(type, out var state))
        {
            return (TState)state;
        }

        var newState = System.Activator.CreateInstance<TState>();

        newState.Parent = this;

        newState.Initialize();

        stateCache[type] = newState;

        return newState;
    }

    public void UpdateScore(SGEvent e)
    {
        var goalEvent = (GoalScored)e;

        if(goalEvent.bIsRedScore)
        {
            redScore++;
        }
        else
        {
            blueScore++;
        }

        SetScoreText(redScore, blueScore);

        float scoreThreshold = Services.gameManager.scoreThreshold;
        if(redScore >= scoreThreshold || blueScore >= scoreThreshold)
        {
            PendingState = TransitionState<StateGameOver>();
        }
    }

    public void SetScoreText(int red, int blue)
    {
        Services.gameManager.scoreText.text = string.Format("Red: {0}   Blue: {1}", red, blue);
    }
}

public abstract class State
{
    public GameState Parent;

    public virtual void Initialize() { }
    public virtual void Update() { }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
}

public class StateMenu : State
{
    public override void OnExit()
    {
        Parent.ResetGameTime();
    }
}

public class StateGameInProgress : State
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void Update()
    {
        base.Update();
        Parent.UpdateGameTime(Time.deltaTime);
        Services.AIManager.Update();
    }
}

public class StateGameOver : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        Services.eventManager.FireEvent(new GameOver());
    }
}
