using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{	
	public float gridSize;
	public GameObject cursor;
	public GameObject pointer;
	public GameObject secondaryPointer;
	// public float edgeWidth = 0.01f;
	// public Material edgeMaterial;
	public InputManager inputManager;
	public GameObject currentMaterialPrefab;
	public Material highlightMaterial;

	private string primaryInteractionButton = "Fire1";

	private List<GameObject> placedMaterials = new List<GameObject>();
	private List<GameObject> uiObjects = new List<GameObject>();
	private GameStateManager gameStateManager;
	private CompoundObject highlightedSelection;
	private Material originalCursorMaterial;
	private GameObject snappedTo;
	private bool snapped = false;
	private IDictionary<GameObject, Material> previousMaterials = new Dictionary<GameObject, Material>();
	private GameObject compoundObjectMenu;
	private GameObject subSelectionTool;
	private bool primaryManipulationActive = false;
	private bool secondaryManipulationActive = false;
	private GameObject manipulationAnchor;
	private GameObject manipulationAnchorTarget;
	private GameObject manipulationLookTarget;

    // Start is called before the first frame update
    void Start() {
		TransformSnap.gridSize = gridSize;
		inputManager.registerListener(primaryInteractionButton, "start", primaryEngagementStarted);
		inputManager.registerListener(primaryInteractionButton, "drag", primaryEngagementDragged);
		inputManager.registerListener(primaryInteractionButton, "stop", primaryEngagementEnded);

		pointer.transform.localScale = Vector3.zero;
        cursor.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		originalCursorMaterial = cursor.GetComponent<Renderer>().material;
		gameStateManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();

		manipulationAnchor = Instantiate(new GameObject(), transform.position, Quaternion.identity);
		manipulationAnchor.AddComponent<Follower>();
		Follower anchorFollower = manipulationAnchor.GetComponent<Follower>();
		anchorFollower.target = pointer;

		compoundObjectMenu = ConstructCompoundObjectMenu();
    }

	GameObject createCurrentMaterialAtCursor(CompoundObject compound = null) {
		return createPrefabAt(currentMaterialPrefab, cursor.transform, true, compound);
	}

	GameObject createPrefabAt(GameObject prefab, Transform transform, bool placedMaterial = true, CompoundObject compound = null) {
		GameObject createdObject = Instantiate(prefab, transform.position, Quaternion.identity);
		createdObject.transform.rotation = transform.rotation;
        createdObject.transform.localScale = new Vector3(gridSize, gridSize, gridSize);
		if(placedMaterial) {
			placedMaterials.Add(createdObject);
			if(compound != null) {
				compound.Add(createdObject);
			}
		}

		return createdObject;
	}

    // Update is called once per frame
    void Update() {
		handleUserActions();
    }

	void handleUserActions() {
		switch(gameStateManager.CurrentControlMode) {
			case ControlMode.Create:
				TransformSnap snap = TransformSnap.SnapToClosest(pointer, placedMaterials);
				cursor.transform.position = snap.transform.position;
				cursor.transform.rotation = snap.transform.rotation;
				snapped = snap.snapped;
				if(snap.snapped) {
					cursor.GetComponent<Renderer>().material = highlightMaterial;
					snappedTo = snap.target;
				} else {
					cursor.GetComponent<Renderer>().material = originalCursorMaterial;
				}
				break;
			case ControlMode.Edit:
			case ControlMode.Manipulate:
				GameObject possibleSelection = TransformSnap.GetClosestObject(pointer, placedMaterials);
				if(possibleSelection == null) {
					ClearHighlightedSelection();
				} else {
					if(highlightedSelection != null) {
						ClearHighlightedSelection();
					}
					CompoundObject compoundSelection = CompoundObject.GetCompoundFor(possibleSelection);
					if(compoundSelection != highlightedSelection) {
						HighlightSelection(compoundSelection);
					}
				}
				break;
			/*case ControlMode.Manipulate:
				GameObject possibleTool = TransformSnap.GetClosestObject(pointer, uiObjects);
				if(possibleTool == null) {
					ClearHighlightedSelection();
				} else {
					if(highlightedSelection != null){
						ClearHighlightedSelection();
					}
					CompoundObject compoundSelection = CompoundObject.GetCompoundFor(possibleTool);
					if(compoundSelection != highlightedSelection) {
						HighlightSelection(compoundSelection);
					}
				}
				break;*/
		}
	}

	void adoptTool(Tool tool, Collision collision) {
		// Debug.Log("Adopting tool " + tool.name);
	}

	void ClearHighlightedSelection() {
		if(highlightedSelection == null) { return; }

		foreach(GameObject gameObject in highlightedSelection.objects) {
			gameObject.GetComponent<Renderer>().material = previousMaterials[gameObject];
		}
		highlightedSelection = null;
	}

	void HighlightSelection(CompoundObject selection) {
		highlightedSelection = selection;

		foreach(GameObject gameObject in selection.objects) {
			if(gameObject.GetComponent<Renderer>().material != highlightMaterial) {
				previousMaterials[gameObject] = gameObject.GetComponent<Renderer>().material;
			}

			gameObject.GetComponent<Renderer>().material = highlightMaterial;
		}
	}

	void startManipulation(string engagementType) {
		switch(engagementType) {
			case "primary":
				if(secondaryManipulationActive) {
					startTwoControllerManipulation();
				} else {
					startPrimaryManipulation();
				}
				break;
			case "secondary":
				if(primaryManipulationActive) {
					startTwoControllerManipulation();
				} else {
					startSecondayManipuation();
				}
				break;
		}	
	}

	void startPrimaryManipulation() {
		primaryManipulationActive = true;
		highlightedSelection.getParent().transform.parent = manipulationAnchor.transform;
	}

	void startSecondayManipuation() {
		secondaryManipulationActive = true;
	}

	void startTwoControllerManipulation() {
		primaryManipulationActive = true;
		secondaryManipulationActive = true;
	}

	void endManipulation(string engagementType) {
		switch(engagementType) {
			case "primary":
				if(secondaryManipulationActive) {
					endTwoControllerManipulation();
					startSecondayManipuation();
				} else {
					endPrimaryManipulation();
				}
				break;
			case "secondary":
				if(primaryManipulationActive) {
					endTwoControllerManipulation();
					startPrimaryManipulation();
				} else {
					endSecondaryManipulation();
				}
				break;
		}
	}

	void endPrimaryManipulation() {
		primaryManipulationActive = false;
		highlightedSelection.getParent().transform.parent = transform;
	}

	void endSecondaryManipulation() {
		secondaryManipulationActive = false;
	}

	void endTwoControllerManipulation() {
		primaryManipulationActive = false;
		secondaryManipulationActive = false;
	}	public float solidifyTemperature;


	void primaryEngagementStarted() {
		switch(gameStateManager.CurrentControlMode) {
			case ControlMode.Manipulate:
				startManipulation("primary");
				break;
		}
	}

	void primaryEngagementDragged() {
		// Debug.Log("primaryEngagementDragged");
	}

	void primaryEngagementEnded() {
		// Debug.Log("primaryEngagementEnded");
		switch(gameStateManager.CurrentControlMode) {
			case ControlMode.Create:
				createCurrentMaterialAtCursor(snapped ? CompoundObject.GetCompoundFor(snappedTo) : null);
				break;
			case ControlMode.Edit:
				editCurrentHighlight();
				break;
			case ControlMode.Manipulate:
				endManipulation("primary");
				break;
		}
	}

	void editCurrentHighlight() {
		// Debug.Log("editCurrentHighlight()...");
		// highlightedSelection.objects[0].GetComponent<Emitter>().setFrequency(600);
		highlightedSelection.objects[0].GetComponent<Emitter>().turnOn();
	}

	GameObject ConstructCompoundObjectMenu() {
		GameObject menu = Instantiate(new GameObject(), new Vector3(0, 1f, 0), Quaternion.identity);
		
		GameObject subSelectionTool = Instantiate(new GameObject(), menu.transform.position, Quaternion.identity);
		subSelectionTool.transform.parent = menu.transform;
		
		GameObject cube = createPrefabAt(currentMaterialPrefab, menu.transform, false);
		cube.transform.parent = subSelectionTool.transform;
		cube = createPrefabAt(currentMaterialPrefab, menu.transform, false);
		cube.transform.position += new Vector3(0, 0, gridSize);
		cube.transform.parent = subSelectionTool.transform;
		cube.GetComponent<Renderer>().material = highlightMaterial;
		cube = createPrefabAt(currentMaterialPrefab, menu.transform, false);
		cube.transform.position += (new Vector3(0, 0, gridSize) * 2f);
		cube.transform.parent = subSelectionTool.transform;
		
		subSelectionTool.transform.localScale *= 0.3f;
		uiObjects.Add(subSelectionTool);
		subSelectionTool.AddComponent<Tool>();
		
		return menu;
	}
}
