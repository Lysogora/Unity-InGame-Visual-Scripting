using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

[System.Serializable]
public class NodeNT : INTNode<INTData<BaseElement>, INTOperation<INTData<BaseElement>, IOpModNT>> {
	#region INTNode implementation
	public LogicOperator logicOperator;
	public LogicOperator LogicOperator {
		get {return logicOperator;}
		set {logicOperator = value;}
	}

	public float weight;
	public float Weight {
		get {return weight;}
		set {weight = value;}
	}
	public INTNode<INTData<BaseElement>, INTOperation<INTData<BaseElement>, IOpModNT>> higherNode;
	public INTNode<INTData<BaseElement>, INTOperation<INTData<BaseElement>, IOpModNT>> HigherNode {
		get {return higherNode;}
		set {higherNode = value;}
	}
	public List<INTNode<INTData<BaseElement>, INTOperation<INTData<BaseElement>, IOpModNT>>> lowerNodes;
	public List<INTNode<INTData<BaseElement>, INTOperation<INTData<BaseElement>, IOpModNT>>> LowerNodes {
		get {return lowerNodes;}
		set {lowerNodes = value;}
	}
	public List<INTOperation<INTData<BaseElement>, IOpModNT>> operations = new List<INTOperation<INTData<BaseElement>, IOpModNT>> ();
	public List<INTOperation<INTData<BaseElement>, IOpModNT>> Operations {
		get {return operations;}
		set {operations = value;}
	}

	public List<INTData<BaseElement>> datum = new List<INTData<BaseElement>> ();
	public List<INTData<BaseElement>> Datum {
		get {return datum;}
		set {datum = value;}
	}

	public virtual void Initialize (){
		
	}

	public virtual void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{
		Datum = _objects;
		for (int i = 0; i < Operations.Count; i++) {
			Operations[i].ProcessData(_subjects, _objects);
		}	
	}

	#endregion



}
