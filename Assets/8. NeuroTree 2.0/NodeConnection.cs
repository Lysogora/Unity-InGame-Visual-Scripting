using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DataDirection {IncomeData, OutcomeData}

[System.Serializable]
public class NodeConnection {
	public BaseNode node;
	public DataDirection dataDirection;
	public VarType varType;
	public int num;
	public List <NodeConnection> oppositeConnections = new List<NodeConnection> ();

	public void InitializeConnection(BaseNode _node, DataDirection _direction, VarType _varType, int _num){
		node = _node;
		dataDirection = _direction;
		varType = _varType;
		num = _num;
	}

}
