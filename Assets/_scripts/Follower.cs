using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
	public GameObject target;
	public bool followPosition = true;
	public bool followRotation = true;
	
    void Start()
    {
        
    }

    void Update()
    {
		if(followPosition)
			transform.position = target.transform.position;
		if(followRotation)
			transform.rotation = target.transform.rotation;
    }
}
