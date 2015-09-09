using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class CloseToValOperationNT : OperationNT {

	public override void Initialize (){
		ModType = OpModType.closeToVal;
	}
	
	public override void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{

		
	}
}
