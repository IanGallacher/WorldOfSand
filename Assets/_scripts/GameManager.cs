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
	public Material highlightMaterial;
	
	private string primaryInteractionButton = "Fire1";
	
	private List<GameObject> placedMaterials = new List<GameObject>();
	private GameStateManager gameStateManager;
	private GameObject highlightedSelection;
	private Material previousSelectionMaterial;
	
    // Start is called before the first frame update
    void Start() {
		TransformSnap.gridSize = gridSize;
		inputManager.registerListener(primaryInteractionButton, "start", primaryEngagementStarted);
		inputManager.registerListener(primaryInteractionButton, "drag", primaryEngagementDragged);
		inputManager.registerListener(primaryInteractionButton, "stop", primaryEngagementEnded);
		inputManager.registerListener("Mouse ScrollWheel", "stop", mouseScrollWheelStopped);
		
		pointer.transform.localScale = Vector3.zero;
        cursor.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		gameStateManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
    }
	
	GameObject createCurrentMaterialAtCursor(){
		return createPrefabAt(currentMaterialPrefab, cursor.transform);
	}
	
	GameObject createPrefabAt(GameObject prefab, Transform transform, bool placedMaterial = true) {
		GameObject createdObject = Instantiate(prefab, transform.position, Quaternion.identity);
		createdObject.transform.rotation = transform.rotation;
        createdObject.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		if(placedMaterial)
			placedMaterials.Add(createdObject);
		
		return createdObject;
	}

    // Update is called once per frame
    void Update() {
		/*Vector3 newCursorPosition = SnapPosition(pointer.transform.position);
		if(newCursorPosition != cursor.transform.position){
			inputManager.CursorMoved();
		}*/
		switch(gameStateManager.CurrentControlMode) {
			case ControlMode.Create:
				TransformSnap snap = TransformSnap.SnapToClosest(pointer, placedMaterials);
				cursor.transform.position = snap.transform.position;
				cursor.transform.rotation = snap.transform.rotation;
				if(snap.snapped){
					// TODO: Indicate snapping
				}
				break;
			case ControlMode.Edit:
				GameObject possibleSelection = TransformSnap.GetClosestObject(pointer, placedMaterials);
				if(possibleSelection != null && possibleSelection != highlightedSelection) {
					ClearHighlightedSelection();
					HighlightSelection(possibleSelection);
				}
				break;
		}
    }
	
	void ClearHighlightedSelection(){
		if(highlightedSelection == null)
			return;
		
		highlightedSelection.GetComponent<Renderer>().material = previousSelectionMaterial;
	}
	
	void HighlightSelection(GameObject selection){
		Debug.Log("HighlightSelection");
		highlightedSelection = selection;
		previousSelectionMaterial = selection.GetComponent<Renderer>().material;
		selection.GetComponent<Renderer>().material = highlightMaterial;
	}
	
	void primaryEngagementStarted() {
		// Debug.Log("primaryEngagementStarted");
	}
	
	void primaryEngagementDragged() {
		// Debug.Log("primaryEngagementDragged");
	}
	
	void primaryEngagementEnded() {
		// Debug.Log("primaryEngagementEnded");
		switch(gameStateManager.CurrentControlMode){
			case ControlMode.Create:
				createCurrentMaterialAtCursor();
				break;
			case ControlMode.Edit:
				editCurrentHighlight();
				break;
		}
	}
	
	void mouseScrollWheelStopped() {
		// Debug.Log("mouseScrollWheelStopped");
		gameStateManager.SetCurrentControlMode(ControlMode.Edit);
	}
	
	void editCurrentHighlight() {
		Debug.Log("editCurrentHighlight()...");
	}
	
	Vector3 SnapPosition(Vector3 position) {
		return position;
	}
}
