using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tool : MonoBehaviour
{
	public GameObject interactor;
	public Action<Tool, Collision> collisionCallback;
	public float squaredActivationRadiusPadding = 0; // Increase the radius of activation beyond the object radius
	public Material inactiveMaterial;
	public Material activeMaterial;
	public Knob behaviour;
	
	private bool active;
	private float squaredRadius;
	private Renderer renderer;
	private Bounds bounds;
	private Vector3 originalScale;
	private Vector3 activeScale;
	
	public Tool(string name, Action<Tool, Collision> collisionCallback) {
		this.name = name;
		this.collisionCallback = collisionCallback;
	}
	
	void Start() {
		squaredRadius = (transform.localScale / 2f).sqrMagnitude + squaredActivationRadiusPadding;
		renderer = GetComponent<Renderer>();
		bounds = renderer.bounds;
		bounds.Expand(squaredActivationRadiusPadding);
		originalScale = transform.localScale;
		activeScale = transform.localScale * 1.1f;
	}
	
	void Update() {
		if(interactor == null)
			return;
		
		Vector3 delta = interactor.transform.position - transform.position;
		// ProcessActive(delta.sqrMagnitude < squaredRadius);
		ProcessActive(bounds.Contains(interactor.transform.position));
	}
	
	void ProcessActive(bool active) {
		if(active){
			if(!this.active)
				Activate();
		} else {
			if(this.active)
				Deactivate();
		}
	}
	
	void Activate(){
		// Debug.Log("Activate()...");
		active = true;
		renderer.material = activeMaterial;
		transform.localScale = activeScale;
		behaviour.Activate();
	}
	
	void Deactivate() {
		// Debug.Log("Deactivate()...");
		active = false;
		renderer.material = inactiveMaterial;
		transform.localScale = originalScale;
		behaviour.Deactivate();
	}
	
	void OnCollisionEnter(Collision collision) {
		// Debug.Log("Collision Entered.");
		collisionCallback(this, collision);
	}
}
