using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Emitter : MonoBehaviour
{
	public GameObject particlePrefab;
	
	private bool on = false;
    private float elapsedTime = 0.0f;
	private float frequency = 60;
    private float waitTime = 2.0f;
	private float force = 5.0f;
	
	void Start(){
	
	}
	
	void Update(){
		if(!on)
			return;
			
		if(frequency <= 0)
			return;
		
		elapsedTime += Time.deltaTime;

        if (elapsedTime > waitTime)
        {
            elapsedTime = elapsedTime - waitTime;
			spawnPrefab();
        }
	}
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log("Collision!");
        /*foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();*/
    }
	
	public void setFrequency(float frequency){
		this.frequency = frequency;
		waitTime = 60 / frequency;
	}
	
	public void spawnPrefab(){
		Debug.Log(frequency);
		GameObject createdObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);
		createdObject.transform.rotation = transform.rotation;
        createdObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		createdObject.GetComponent<Rigidbody>().AddForce(transform.up * -1f * force, ForceMode.Impulse);
	}
	
	public void turnOn(){
		on = true;
	}
	
	public void turnOff(){
		on = false;
	}
	
}
