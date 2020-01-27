using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionControlMode : MonoBehaviour
{
	private GameStateManager _gameStateManager;
	private InputManager _inputManager;
	[SerializeField]
	private GameObject _toolGizmo;
	[SerializeField]
	private ControlMode _newControlMode;

	void Awake()
	{
		_gameStateManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
		_inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

    void OnTriggerEnter(Collider col)
    {
		
         Debug.Log("Switch Collision");
		 
         Debug.Log(col.gameObject.name);
		if(_inputManager.handObjects.Contains(col.gameObject)) {
			_gameStateManager.SetCurrentControlMode(_newControlMode);
			_toolGizmo.SetActive(false);
		}
    }
}
