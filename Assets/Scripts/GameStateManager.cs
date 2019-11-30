using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Maintains a state machine for the current state of the game. Other objects can register actions to run when the state changes
/// Implemented as a singleton, other objects should access it with GameManager.Instance
/// </summary>
public class GameStateManager : MonoBehaviour
{
    // Configuration
    [SerializeField] private GameStates InitialGameState = GameStates.BEFORE_ENTERING_BASE;

    // Singleton implementation
    private static GameStateManager instance;
    public static GameStateManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            throw new System.InvalidOperationException("Cannot get instance of GameManager, make sure it is in the scene");
        }
    }

    // Game state
    public enum GameStates
    {
        NONE,
        BEFORE_ENTERING_BASE,
        BASE_ENTERED,
        GAME_OVER_LOSE,
        GAME_OVER_WIN
    }
    public GameStates GameState { get; private set; }

    // State listeners
    private Dictionary<GameStates, List<Action>> OnStateChangeActions;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There should not be more than one instance of GameManager. Deleting this one");
            Destroy(this);
        }
        instance = this;

        OnStateChangeActions = new Dictionary<GameStates, List<Action>>();
    }

    void Start()
    {
        ChangeState(InitialGameState);
    }

    public void OnBaseEntered()
    {
        if (GameState == GameStates.BEFORE_ENTERING_BASE)
        {
            ChangeState(GameStates.BASE_ENTERED);
        }
    }

    public void OnTimeUp()
    {
        // use the outdoor trigger to determine if the game was won or lost
        OutdoorTrigger outdoorTrigger = FindObjectOfType<OutdoorTrigger>();
        if (outdoorTrigger == null)
        {
            throw new MissingComponentException("An OutDoorTrigger is required to determine if the players got out of the base. Please add one to the scene");
        }

        if (outdoorTrigger.NumberOfPlayersOutsideTheBase < 1)
        {
            ChangeState(GameStates.GAME_OVER_LOSE);
        }
        else
        {
            ChangeState(GameStates.GAME_OVER_WIN);
        }
    }

    public void OnResetGame()
    {
        ChangeState(GameStates.BEFORE_ENTERING_BASE);
    }

    private void ChangeState(GameStates newState)
    {
        bool isAlreadyAtThisState = GameState == newState;
        GameState = newState;
        if (OnStateChangeActions.ContainsKey(newState) && isAlreadyAtThisState == false)
        {
            foreach (Action action in OnStateChangeActions[newState])
            {
                action.Invoke();
            }
        }
    }

    public void RegisterOnStateChange(GameStates stateToListenFor, Action onStateChange)
    {
        if (OnStateChangeActions.ContainsKey(stateToListenFor) == false)
        {
            OnStateChangeActions.Add(stateToListenFor, new List<Action>());
        }
        OnStateChangeActions[stateToListenFor].Add(onStateChange);
    }
}
