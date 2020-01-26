using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum GameState {
	Paused,
	Play,
}

enum GameStateTransition {
	Pause,
	Reset,
	Run,
}

enum ControlMode {
	Edit,
	Erase,
	Manipulate,
}


[System.Serializable]
public class GameStateTransationEvent : UnityEvent<GameStateTransition>
{

}

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameState _currentGameState;

    // Awake is called once for the lifetime of the script, before start.
    // Used for setting up variables.
    void Awake()
    {
        _currentGameState = GameState.Paused;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
