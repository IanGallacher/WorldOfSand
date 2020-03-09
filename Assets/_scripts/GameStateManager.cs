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
    SelectMode,
}


[System.Serializable]
public class GameStateTransitionEvent : UnityEvent<GameStateTransition>
{

}

public class GameStateManager : MonoBehaviour
{
    public GameStateTransitionEvent gameStateTransitionEvent;
    public GameState CurrentGameState { get => _currentGameState; }
	public ControlMode CurrentControlMode { get => _currentControlMode; }
	public InteractableObjectManager _interactableObjectManager;

    [SerializeField]
    private GameState _currentGameState = GameState.Paused;
	[SerializeField]
	private ControlMode _currentControlMode = ControlMode.Create;

	void Awake()
	{
		_interactableObjectManager = GetComponent<InteractableObjectManager>();
	}
	
	public void SetCurrentControlMode(ControlMode controlMode)
	{
		_currentControlMode = controlMode;
	}

	public void PauseGame()
	{
		if(_currentGameState == GameState.Paused) { return; }
		_currentGameState = GameState.Paused;

		// TODO: Use events for the following:
		_interactableObjectManager.ExitPlayMode();
	}

	public void PlayGame()
	{
		if(_currentGameState == GameState.Play) { return; }
		_currentGameState = GameState.Play;
		
		// TODO: Use events for the following:
		_interactableObjectManager.EnterPlayMode();
	}

    void TransitionGameState(GameStateTransition transition)
	{
        gameStateTransitionEvent.Invoke(transition);
    }
}
