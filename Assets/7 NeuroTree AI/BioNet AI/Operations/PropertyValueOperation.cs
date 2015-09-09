using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class PropertyValueOperation : OperationNT {
	
	public override void Initialize (){
		
	}
	
	public override void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{
		switch (ModType) {
		case OpModType.NotValue:
			for (int i = 0; i < _objects.Count; i++) {
				if(_objects[i].ObjectNT.elProperties.ContainsKey((PropertyType)EnumTypeVal)){
					if(_objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].val != IntValue){					
						_objects[i].Weight += 1.0f*Weight;
						Debug.Log("Not from out team");
					}
				}			
			}
			break;
		case OpModType.ExactVal:
			for (int i = 0; i < _objects.Count; i++) {
				if(_objects[i].ObjectNT.elProperties.ContainsKey((PropertyType)EnumTypeVal)){
					if(_objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].val == IntValue){					
						_objects[i].Weight += 1.0f*Weight;
					}
				}			
			}
			break;
		}
		
		
	}
}
