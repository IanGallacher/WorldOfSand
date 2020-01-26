using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState {
	Paused,
	Play,
}

public enum GameStateTransition {
	Pause,
	Reset,
	Run,
}

public enum ControlMode {
	Create,
	Edit,
	Erase,
	Manipulate,
}


[System.Serializable]
public class GameStateTransitionEvent : UnityEvent<GameStateTransition>
{

}

public class GameStateManager : MonoBehaviour
{
    public GameStateTransitionEvent gameStateTransitionEvent;

    [SerializeField]
    private GameState _currentGameState;
	[SerializeField]
	private ControlMode _currentControlMode = ControlMode.Create;

    public GameState CurrentGameState 
    {
        get => _currentGameState;
    }
	
	public ControlMode CurrentControlMode
	{
		get => _currentControlMode;
	}
	
	public void SetCurrentControlMode(ControlMode controlMode){
		_currentControlMode = controlMode;
	}

    // Awake is called once for the lifetime of the script, before start.
    // Used for setting up variables.
    void Awake()
    {
        _currentGameState = GameState.Paused;
    }

    void TransitionGameState(GameStateTransition transition) {
        gameStateTransitionEvent.Invoke(transition);
    }
}
