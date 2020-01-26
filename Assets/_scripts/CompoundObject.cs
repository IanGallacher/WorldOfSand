using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundObject
{
	private static IDictionary<GameObject, CompoundObject> compoundObjects = new Dictionary<GameObject, CompoundObject>();
	
	public List<GameObject> objects = new List<GameObject>();
	
	public CompoundObject(GameObject gameObject){
		Add(gameObject);
	}
	
	public static CompoundObject GetCompoundFor(GameObject gameObject){
		if(gameObject == null)
			return null;
		
		if(compoundObjects.ContainsKey(gameObject))
			return compoundObjects[gameObject];
		
		return new CompoundObject(gameObject);
	}
	
	public void Add(GameObject gameObject){
		objects.Add(gameObject);
		compoundObjects.Add(gameObject, this);
	}
}
