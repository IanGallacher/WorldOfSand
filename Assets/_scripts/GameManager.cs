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
	private CompoundObject highlightedSelection;
	private Material originalCursorMaterial;
	private GameObject snappedTo;
	private bool snapped = false;
	private IDictionary<GameObject, Material> previousMaterials = new Dictionary<GameObject, Material>();
	
    // Start is called before the first frame update
    void Start() {
		TransformSnap.gridSize = gridSize;
		inputManager.registerListener(primaryInteractionButton, "start", primaryEngagementStarted);
		inputManager.registerListener(primaryInteractionButton, "drag", primaryEngagementDragged);
		inputManager.registerListener(primaryInteractionButton, "stop", primaryEngagementEnded);
		inputManager.registerListener("Mouse ScrollWheel", "start", mouseScrollWheelStarted);
		
		pointer.transform.localScale = Vector3.zero;
        cursor.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		originalCursorMaterial = cursor.GetComponent<Renderer>().material;
		gameStateManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
    }
	
	GameObject createCurrentMaterialAtCursor(CompoundObject compound = null){
		return createPrefabAt(currentMaterialPrefab, cursor.transform, true, compound);
	}
	
	GameObject createPrefabAt(GameObject prefab, Transform transform, bool placedMaterial = true, CompoundObject compound = null) {
		GameObject createdObject = Instantiate(prefab, transform.position, Quaternion.identity);		
		createdObject.transform.rotation = transform.rotation;
        createdObject.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		if(placedMaterial){
			placedMaterials.Add(createdObject);
			if(compound != null){
				compound.Add(createdObject);
			}
		}
		
		return createdObject;
	}

    // Update is called once per frame
    void Update() {
		/*Vector3 newCursorPosition = SnapPosition(pointer.transform.position);
		if(newCursorPosition != cursor.transform.position){
			inputManager.CursorMoved();
		}*/
		handleUserActions();
    }
	
	void handleUserActions(){
		switch(gameStateManager.CurrentControlMode) {
			case ControlMode.Create:
				TransformSnap snap = TransformSnap.SnapToClosest(pointer, placedMaterials);
				cursor.transform.position = snap.transform.position;
				cursor.transform.rotation = snap.transform.rotation;
				snapped = snap.snapped;
				if(snap.snapped){
					cursor.GetComponent<Renderer>().material = highlightMaterial;
					snappedTo = snap.target;
				} else {
					cursor.GetComponent<Renderer>().material = originalCursorMaterial;
				}
				break;
			case ControlMode.Edit:
				GameObject possibleSelection = TransformSnap.GetClosestObject(pointer, placedMaterials);
				if(possibleSelection == null) {
					ClearHighlightedSelection();
				} else {
					if(highlightedSelection != null){
						ClearHighlightedSelection();
					}
					CompoundObject compoundSelection = CompoundObject.GetCompoundFor(possibleSelection);
					if(compoundSelection != highlightedSelection) {
						HighlightSelection(compoundSelection);
					}
				}
				break;
		}
	}
	
	void ClearHighlightedSelection(){
		if(highlightedSelection == null)
			return;
		
		foreach(GameObject gameObject in highlightedSelection.objects){
			gameObject.GetComponent<Renderer>().material = previousMaterials[gameObject];
		}
		highlightedSelection = null;
	}
	
	void HighlightSelection(CompoundObject selection){
		highlightedSelection = selection;
		
		foreach(GameObject gameObject in selection.objects){
			if(gameObject.GetComponent<Renderer>().material != highlightMaterial)
				previousMaterials[gameObject] = gameObject.GetComponent<Renderer>().material;
			
			gameObject.GetComponent<Renderer>().material = highlightMaterial;
		}
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
				createCurrentMaterialAtCursor(snapped ? CompoundObject.GetCompoundFor(snappedTo) : null);
				break;
			case ControlMode.Edit:
				editCurrentHighlight();
				break;
		}
	}
	
	void mouseScrollWheelStarted() {
		Debug.Log("mouseScrollWheelStarted");
		// gameStateManager.SetCurrentControlMode(ControlMode.Create);
		Debug.Log(gameStateManager.IncrementControlMode());
	}
	
	void editCurrentHighlight() {
		Debug.Log("editCurrentHighlight()...");
	}
	
	Vector3 SnapPosition(Vector3 position) {
		return position;
	}
}
