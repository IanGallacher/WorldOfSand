using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
	GameObject controller;
	GameObject subController;
	bool active = false;
	float startingZ;
	float startingControllerZ;
	float minimumRotation = -30;
	float maximumRotation = 210;
	
    void Start()
    {
        subController = new GameObject();
    }

    void Update()
    {
        if(!active)
			return;
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, startingZ + controller.transform.eulerAngles.z - startingControllerZ);
    }
	
	public void Activate(){
		active = true;
		
		controller = GetComponent<Tool>().interactor;
		startingZ = transform.eulerAngles.z;
		startingControllerZ = controller.transform.eulerAngles.z;
	}
	
	public void Deactivate(){
		active = false;
	}
}
