using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DataDirection {IncomeData, OutcomeData}

[System.Serializable]
public class VarPass {
	public int connectionID;
	public BaseNode node;
	public DataDirection dataDirection;
	public VarType varType;
	public int num;
	public List <VarPass> oppositeConnections = new List<VarPass> ();

	public void InitializeConnection(BaseNode _node, DataDirection _direction, VarType _varType, int _num){
		node = _node;
		dataDirection = _direction;
		varType = _varType;
		num = _num;
	}

}
