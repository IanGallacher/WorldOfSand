﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSnap
{	
	public Transform transform;
	public bool snapped;
	public GameObject target;
	
	public static float gridSize;
	
	private static GameObject transformHolder = new GameObject();
	
	public static TransformSnap SnapToClosest(GameObject subject, List<GameObject> targets, float maximumSquaredDistance = 0){
		GameObject bestTarget = GetClosestObject(subject, targets, maximumSquaredDistance);
		if(bestTarget == null){
			return Unsnapped(subject.transform);
		}
		return SnapTo(subject, bestTarget);
	}
	
	public static GameObject GetClosestObject(GameObject subject, List<GameObject> targets, float maximumSquaredDistance = 0){
		return GetClosestObject(subject.transform.position, targets, maximumSquaredDistance);
	}
	
	public static GameObject GetClosestObject(Vector3 currentPosition, List<GameObject> targets, float maximumSquaredDistance = 0){
		if(maximumSquaredDistance == 0)
			maximumSquaredDistance = gridSize * gridSize * 2;
		GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach(GameObject target in targets)
        {
            Vector3 directionToTarget = target.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = target;
            }
        }
		if(closestDistanceSqr <= maximumSquaredDistance){
			return bestTarget;
		}
		return null;
	}
	
	public static TransformSnap SnapTo(GameObject subject, GameObject target){
		float cubeApothem = gridSize / 2;
		
		TransformSnap snap = new TransformSnap();
		snap.snapped = true;
		snap.transform = transformHolder.transform;
		Vector3 directionToTarget = target.transform.position - subject.transform.position;
		Vector3 upPosition = target.transform.position + target.transform.up * cubeApothem;
		Vector3 leftPosition = target.transform.position - target.transform.right * cubeApothem;
		Vector3 backPosition = target.transform.position - target.transform.forward * cubeApothem;
		Vector3 rightPosition = target.transform.position + target.transform.right * cubeApothem;
		Vector3 forwardPosition = target.transform.position + target.transform.forward * cubeApothem;
		Vector3 downPosition = target.transform.position - target.transform.up * cubeApothem;
		
		Vector3 upDistance = upPosition - subject.transform.position;
		Vector3 leftDistance = leftPosition - subject.transform.position;
		Vector3 backDistance = backPosition - subject.transform.position;
		Vector3 rightDistance = rightPosition - subject.transform.position;
		Vector3 forwardDistance = forwardPosition - subject.transform.position;
		Vector3 downDistance = downPosition - subject.transform.position;
		
		Vector3 smallest = upDistance;
		Vector3 direction = target.transform.up;
		if(leftDistance.sqrMagnitude < smallest.sqrMagnitude){
			smallest = leftDistance;
			direction = target.transform.right * -1f;
		}
		if(backDistance.sqrMagnitude < smallest.sqrMagnitude){
			smallest = backDistance;
			direction = target.transform.forward * -1f;
		}
		if(rightDistance.sqrMagnitude < smallest.sqrMagnitude){
			smallest = rightDistance;
			direction = target.transform.right;
		}
		if(forwardDistance.sqrMagnitude < smallest.sqrMagnitude){
			smallest = forwardDistance;
			direction = target.transform.forward;
		}
		if(downDistance.sqrMagnitude < smallest.sqrMagnitude){
			smallest = downDistance;
			direction = target.transform.up * -1f;
		}
		// direction = Vector3.Normalize(direction);
		
		snap.transform.rotation = target.transform.rotation;
		snap.transform.position = target.transform.position + direction * gridSize;
		snap.target = target;
		return snap;
	}
	
	static TransformSnap Unsnapped(Transform transform){
		TransformSnap snap = new TransformSnap();
		snap.snapped = false;
		snap.transform = transform;
		return snap;
	}
}
