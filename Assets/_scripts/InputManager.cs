using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
	private Dictionary<string, Dictionary<string, Action>> listeners;
	private Dictionary<string, bool> active;

	[SerializeField]
	private GameObject _toolGizmo;
	[SerializeField]
	private GameObject _playerPosition;

	public List<GameObject> handObjects;

    void Awake()
    {
        listeners = new Dictionary<string, Dictionary<string, Action>>();
		active = new Dictionary<string, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<string, Dictionary<string, Action>> axis in listeners){
			process(axis.Key, Input.GetAxis(axis.Key) > 0);
		}
    }

	public void RightTriggerFired() {
		InstantiateGizmo();
	}

	public void LeftTriggerFired() {
		InstantiateGizmo();
	}

	public void ControllerSqueezed() {
		InstantiateGizmo();
	}

	private void InstantiateGizmo() {
		// if(_currentGizmo == null)
		// 	_currentGizmo = Instantiate(_toolGizmo, new Vector3(0,0,0), Quaternion.identity);
		// else
		// _currentGizmo.GetComponent<Transform>().position = hs.GetComponent<Transform>().position;
		_toolGizmo.transform.position = _playerPosition.transform.position;
	}
	
	public void registerListener(string axis, string action, Action callback){
		if(!listeners.ContainsKey(axis))
			listeners.Add(axis, new Dictionary<string, Action>());
		
		if(!active.ContainsKey(axis))
			active.Add(axis, false);
		
		if(!listeners[axis].ContainsKey(action))
			listeners[axis].Add(action, callback);
		
		listeners[axis][action] = callback;
	}
	
	public void CursorMoved(){
        foreach(KeyValuePair<string, Dictionary<string, Action>> axis in listeners){
			if(active[axis.Key] && axis.Value.ContainsKey("drag"))
				axis.Value["drag"]();
		}
	}
	
	void process(string axis, bool down){
		if(active[axis]){
			if(down){
				continueAxis(axis);
			} else{
				stopAxis(axis);
			}
		} else{
			if(down){
				startAxis(axis);
			}
		}
		active[axis] = down;
	}
	
	void startAxis(string axis){
		if(listeners[axis].ContainsKey("start"))
			listeners[axis]["start"]();
	}
	
	void continueAxis(string axis){
		if(listeners[axis].ContainsKey("continue"))
			listeners[axis]["continue"]();
	}
	
	void stopAxis(string axis){
		if(listeners[axis].ContainsKey("stop"))
			listeners[axis]["stop"]();
	}
}
