using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tool : MonoBehaviour
{
	public Action<Tool, Collision> collisionCallback;
	
	public Tool(string name, Action<Tool, Collision> collisionCallback){
		this.name = name;
		this.collisionCallback = collisionCallback;
	}
	
	void Start() {
		gameObject.AddComponent<BoxCollider>();
	}
	
	void OnCollisionEnter(Collision collision) {
		collisionCallback(this, collision);
	}
}
