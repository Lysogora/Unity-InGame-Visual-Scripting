using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class PropertyShareOperation : OperationNT {
	
	public override void Initialize (){

	}
	
	public override void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{
		switch (ModType) {
		case OpModType.closeToVal:
			for (int i = 0; i < _objects.Count; i++) {
				if(_objects[i].ObjectNT.elProperties.ContainsKey((PropertyType)EnumTypeVal)){
					float share = _objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].val / 
						_objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].maxVal;
					_objects[i].Weight += (1 - (Mathf.Abs(FloatValue - share)))*Weight;
				}			
			}
			break;
		case OpModType.ExactVal:
			for (int i = 0; i < _objects.Count; i++) {
				if(_objects[i].ObjectNT.elProperties.ContainsKey((PropertyType)EnumTypeVal)){
					float share = _objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].val / 
						_objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].maxVal;
					if((Mathf.Abs(FloatValue - share)) < 0.05f)
						_objects[i].Weight += 1.0f*Weight;
				}			
			}
			break;
		case OpModType.LessVal:
			for (int i = 0; i < _objects.Count; i++) {
				if(_objects[i].ObjectNT.elProperties.ContainsKey((PropertyType)EnumTypeVal)){
					float share = _objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].val / 
						_objects[i].ObjectNT.elProperties[(PropertyType)EnumTypeVal].maxVal;
					if(FloatValue > share)
						_objects[i].Weight += 1.0f*Weight;
				}			
			}
			break;
		}


	}
}
