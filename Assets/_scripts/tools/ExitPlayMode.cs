using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlayMode : MonoBehaviour
{
	private GameStateManager _gameStateManager;
	private InputManager _inputManager;
	[SerializeField]
	private GameObject _toolGizmo;

	void Awake()
	{
		_gameStateManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
		_inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

    void OnTriggerEnter(Collider col)
    {
		if(_inputManager.handObjects.Contains(col.gameObject)) {
            _gameStateManager.PlayGame();
			_toolGizmo.SetActive(false);
		}
    }
}
