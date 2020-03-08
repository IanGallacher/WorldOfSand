using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Reactor : MonoBehaviour
{	
	public bool interactsWithHeat;
	public float heat;
	public float freezeTemperature;
	public float thawTemperature;
	public float solidifyTemperature;
	public float meltTemperature;
	public float burnTemperature;

	public float heatTransferRate;

	public void ReactWith(GameObject o) {
		Reactor otherReactor = o.GetComponent<Reactor>();
		if(otherReactor == null) { return; }
		
		transferHeat(otherReactor);
	}

	public void transferHeat(Reactor otherReactor) {
		if(!otherReactor.interactsWithHeat) { return; }

		float heatToTransfer = (heat - otherReactor.heat) * heatTransferRate * otherReactor.heatTransferRate * Time.deltaTime;
	}
}
