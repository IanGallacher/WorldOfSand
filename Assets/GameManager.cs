﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float gridSize;
	public GameObject cursor;
	public GameObject pointer;
	public float edgeWidth = 0.01f;
	public Material edgeMaterial;
	public InputManager inputManager;
	
    // Start is called before the first frame update
    void Start()
    {
		inputManager.registerListener("PrimaryEngagement", "start", primaryEngagementStarted);
		inputManager.registerListener("PrimaryEngagement", "drag", primaryEngagementDragged);
		inputManager.registerListener("PrimaryEngagement", "stop", primaryEngagementEnded);
		pointer.transform.localScale = Vector3.zero;
        cursor.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		
    }

    // Update is called once per frame
    void Update()
    {
		/*Vector3 newCursorPosition = SnapPosition(pointer.transform.position);
		if(newCursorPosition != cursor.transform.position){
			inputManager.CursorMoved();
		}*/
		cursor.transform.position = SnapPosition(pointer.transform.position);
		cursor.transform.rotation = pointer.transform.rotation;
    }
	
	void primaryEngagementStarted(){
		Debug.Log("primaryEngagementStarted");
	}
	
	void primaryEngagementDragged(){
		Debug.Log("primaryEngagementDragged");
	}
	
	void primaryEngagementEnded(){
		Debug.Log("primaryEngagementEnded");
	}
	
	Vector3 SnapPosition(Vector3 position){
		return position;
	}
}