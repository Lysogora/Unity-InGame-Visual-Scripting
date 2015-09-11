using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_int : BaseNode, IOutputVar<int> {
	#region IOutputVar implementation
	public int dataInt = 0;
	int IOutputVar<int>.outputVar {
		get {return dataInt;}
		set {dataInt = value;}
	}
	#endregion
	
	public override void InitializeNode(){

		if (outConnections.Count < 1) {
			NodeConnection connection = new NodeConnection ();
			outConnections.Add (connection);
			connection.InitializeConnection(this, DataDirection.OutcomeData, VarType.IntVar, 0);
		}
	}
}
