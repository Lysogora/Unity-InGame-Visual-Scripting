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

	public override void InitializeNode(){
		dataMap = topNode.blackboard.dataMaps[FunctionType.Vextraction];
		if (outConnections.Count  == 0) {
			nodeName = "DataMap";
			outConNames.Add(VarType.IdWeightList, new List<string> (){"El Weights"});


			NodeConnection connection = new NodeConnection ();
			outConnections.Add (connection);
			connection.node = this;
			connection.dataDirection = DataDirection.OutcomeData;
			connection.varType = VarType.IdWeightList;
			connection.num = 0;


		}
	}

}
