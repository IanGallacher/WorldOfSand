using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(transform.childCount);
        foreach (Transform child in transform)
		{
		  //child is your child transform
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
