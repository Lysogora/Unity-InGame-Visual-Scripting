using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class DistanceOperation : OperationNT {

	public override void Initialize (){
		
	}
	
	public override void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{
		for (int i = 0; i < _objects.Count; i++) {
			float dist = Vector3.SqrMagnitude(_subjects[0].ObjectNT.transform.position - _objects[i].ObjectNT.transform.position);
			if(dist <= 25 && _objects[i] != _subjects[0]){
				_objects[i].Weight += Weight*(25.0f - dist)/25.0f;
				//Debug.Log("calculated weight "+_objects[i].Weight.ToString()+" at distance "+dist.ToString());
			}

		}
	}
}
