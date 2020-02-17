using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WorldOfSandParticle : MonoBehaviour
{	
	private List<GameObject> nearbyObjects = new List<GameObject>();
	
	
	void Start(){
	
	}
	
	void Update(){
		foreach(GameObject nearby in nearbyObjects){
			GetComponent<Reactor>().ReactWith(nearby);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		
    }

	void OnTriggerEnter(Collider other)
    {
        nearbyObjects.Add(other.gameObject);
    }
 
    void OnTriggerExit(Collider other)
    {
        nearbyObjects.Remove(other.gameObject);
    }
}
