using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_DataMap : BaseNode, IOutputList<IdWeight> {
	#region IOutputList implementation
	public List<IdWeight> dataMap = new List<IdWeight> ();
	List<IdWeight> IOutputList<IdWeight>.outputList {
		get {return dataMap;}
		set {dataMap = value;}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}

	public override void InitializeNode(){

		dataMap = topNode.blackboard.dataMaps[FunctionType.Vextraction];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
