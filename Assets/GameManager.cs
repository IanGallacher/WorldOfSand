using System.Collections;
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
		
		/*Vector3[] vectors = new Vector3[27];
		int vectorIndex = 0;
		for(int i = -1; i < 2; i++){
			for(int j = -1; j < 2; j++){
				for(int k = -1; k < 2; k++){
					vectors[vectorIndex] = new Vector3(gridSize * i, gridSize * j, gridSize * k);
					vectorIndex++;
				}
			}
		}
		
		for(int i = 0; i < vectors.Length; i++){
			createEdgeCube(vectors[i]);
		}*/
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 newCursorPosition = SnapPosition(pointer.transform.position);
		if(newCursorPosition != cursor.transform.position){
			inputManager.CursorMoved();
		}
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
		//return new Vector3(SnapValue(position.x), SnapValue(position.y), SnapValue(position.z));
	}
	
	float SnapValue(float v){
		return (float) Math.Round(v / gridSize) * gridSize;
	}
	
	GameObject createEdgeCube(Vector3 v){
		GameObject cube = new GameObject("Holder");
		cube.transform.parent = cursor.transform;
		cube.transform.position = v;
		cube.transform.localScale = new Vector3(1f, 1f, 1f);
		
		float edgeLength = gridSize * gridSize;
		
		GameObject bar0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar0.transform.parent = cube.transform;
		bar0.transform.position = new Vector3(-edgeLength, -edgeLength, 0) + v;
		bar0.transform.localScale = new Vector3(edgeWidth, edgeWidth, 1);
		
		GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(-0, -edgeLength, -edgeLength) + v;
		bar.transform.localScale = new Vector3(1, edgeWidth, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(edgeLength, -edgeLength, 0) + v;
		bar.transform.localScale = new Vector3(edgeWidth, edgeWidth, 1);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(0, -edgeLength, edgeLength) + v;
		bar.transform.localScale = new Vector3(1, edgeWidth, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		//Sides
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(-edgeLength, 0, -edgeLength) + v;
		bar.transform.localScale = new Vector3(edgeWidth, 1, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(edgeLength, 0, -edgeLength) + v;
		bar.transform.localScale = new Vector3(edgeWidth, 1, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(edgeLength, 0, edgeLength) + v;
		bar.transform.localScale = new Vector3(edgeWidth, 1, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(-edgeLength, 0, edgeLength) + v;
		bar.transform.localScale = new Vector3(edgeWidth, 1, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		
		//Top
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(-edgeLength, edgeLength, 0) + v;
		bar.transform.localScale = new Vector3(edgeWidth, edgeWidth, 1);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(-0, edgeLength, -edgeLength) + v;
		bar.transform.localScale = new Vector3(1, edgeWidth, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(edgeLength, edgeLength, 0) + v;
		bar.transform.localScale = new Vector3(edgeWidth, edgeWidth, 1);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bar.transform.parent = cube.transform;
		bar.transform.position = new Vector3(0, edgeLength, edgeLength) + v;
		bar.transform.localScale = new Vector3(1, edgeWidth, edgeWidth);
		bar.GetComponent<Renderer>().material = edgeMaterial;
		
		return cube;
	}
}
