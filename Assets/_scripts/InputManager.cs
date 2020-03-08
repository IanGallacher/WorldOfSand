using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
	private Dictionary<string, Dictionary<string, Action>> listeners;
	private Dictionary<string, Dictionary<string, Action>> buttonListeners;
	private Dictionary<string, bool> active;

	[SerializeField]
	private GameObject _toolGizmo;
	[SerializeField]
	private GameObject _playerPosition;

	[SerializeField]
	private List<GameObject> _handObjects;
    public List<GameObject> handObjects { get => _handObjects; }

	[SerializeField]
	private GameObject _camera;
    public List<GameObject> camera { get => camera; }

    void Awake()
    {
        listeners = new Dictionary<string, Dictionary<string, Action>>();
        buttonListeners = new Dictionary<string, Dictionary<string, Action>>();
		active = new Dictionary<string, bool>();
    }

    void Update()
    {
        foreach(KeyValuePair<string, Dictionary<string, Action>> axis in listeners){
			process(axis.Key, Input.GetAxis(axis.Key) > 0);
		}
        foreach(KeyValuePair<string, Dictionary<string, Action>> axis in buttonListeners){
			processButton(axis.Key, Input.GetButton(axis.Key));
		}
    }

	public void RightSqueezeFired() {
		InstantiateModeSelectMenu();
	}

	public void LeftSqueezeFired() {
		InstantiateModeSelectMenu();
	}

	public void RightTriggerFired() {
		InstantiateModeSelectMenu();
	}

	public void LeftTriggerFired() {
		InstantiateModeSelectMenu();
	}

	public void ControllerSqueezed() {
		InstantiateModeSelectMenu();
	}

	private void InstantiateModeSelectMenu() {
		_toolGizmo.transform.position = _playerPosition.transform.position;

		// Vector3 relativePos = _playerPosition.transform.position - _toolGizmo.transform.position;
		// _toolGizmo.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		_toolGizmo.transform.LookAt(_camera.transform);

		_toolGizmo.SetActive(true);
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
	
	public void registerButton(string axis, Action callback){
		registerButton(axis, "up", callback);
	}
	
	public void registerButton(string axis, string action, Action callback){
		if(!buttonListeners.ContainsKey(axis))
			buttonListeners.Add(axis, new Dictionary<string, Action>());
		
		if(!active.ContainsKey(axis))
			active.Add(axis, false);
		
		if(!buttonListeners[axis].ContainsKey(action))
			buttonListeners[axis].Add(action, callback);
		
		buttonListeners[axis][action] = callback;
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
	
	void processButton(string axis, bool down){
		if(active[axis]){
			if(down){
				continueButtonAxis(axis);
			} else{
				stopButtonAxis(axis);
			}
		} else{
			if(down){
				startButtonAxis(axis);
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
	
	void startButtonAxis(string axis){
		if(buttonListeners[axis].ContainsKey("down"))
			buttonListeners[axis]["down"]();
	}
	
	void continueButtonAxis(string axis){
		if(buttonListeners[axis].ContainsKey("continue"))
			buttonListeners[axis]["continue"]();
	}
	
	void stopButtonAxis(string axis){
		if(buttonListeners[axis].ContainsKey("up"))
			buttonListeners[axis]["up"]();
	}
}
