using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class CycleEvasionOperation : OperationNT {
	
	public override void Initialize (){
		
	}
	
	public override void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{
		for (int i = 0; i < _objects.Count; i++) {
			BaseActivityElement bae = _objects[i].ObjectNT as BaseActivityElement;
			if(bae != null){
				if(bae.targets.Contains(_subjects[0].ObjectNT)){
					_objects[i].Weight = -100;
				}
			}			
		}
	}
}
