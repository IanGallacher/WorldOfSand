using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float gridSize;
	public GameObject cursor;
	public GameObject pointer;
	// public float edgeWidth = 0.01f;
	// public Material edgeMaterial;
	public InputManager inputManager;
	public GameObject currentMaterialPrefab;
	
	private string primaryEngagementAxis = "Fire1";
	
	private List<GameObject> placedMaterials = new List<GameObject>();
	
    // Start is called before the first frame update
    void Start()
    {
		TransformSnap.gridSize = gridSize;
		inputManager.registerListener(primaryEngagementAxis, "start", primaryEngagementStarted);
		inputManager.registerListener(primaryEngagementAxis, "drag", primaryEngagementDragged);
		inputManager.registerListener(primaryEngagementAxis, "stop", primaryEngagementEnded);
		pointer.transform.localScale = Vector3.zero;
        cursor.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
    }
	
	GameObject  createCurrentMaterialAtCursor(){
		return createPrefabAt(currentMaterialPrefab, cursor.transform);
	}
	
	GameObject createPrefabAt(GameObject prefab, Transform transform, bool placedMaterial = true){
		GameObject createdObject = Instantiate(prefab, transform.position, Quaternion.identity);
		createdObject.transform.rotation = transform.rotation;
        createdObject.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		if(placedMaterial)
			placedMaterials.Add(createdObject);
		
		return createdObject;
	}

    // Update is called once per frame
    void Update()
    {
		/*Vector3 newCursorPosition = SnapPosition(pointer.transform.position);
		if(newCursorPosition != cursor.transform.position){
			inputManager.CursorMoved();
		}*/
		TransformSnap snap = TransformSnap.SnapToClosest(pointer, placedMaterials);
		cursor.transform.position = snap.transform.position;
		cursor.transform.rotation = snap.transform.rotation;
		if(snap.snapped){
			// Indicate snapping
		}
    }
	
	void primaryEngagementStarted(){
		Debug.Log("primaryEngagementStarted");
	}
	
	void primaryEngagementDragged(){
		Debug.Log("primaryEngagementDragged");
	}
	
	void primaryEngagementEnded(){
		Debug.Log("primaryEngagementEnded");
		createCurrentMaterialAtCursor();
	}
	
	Vector3 SnapPosition(Vector3 position){
		return position;
	}
}
