using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuTree2;

public class BaseNode : IUniNode {
	#region IUniNode implementation
	public LogicNode _logicNode;
	public virtual LogicNode logicNode {
		get {return _logicNode;}
		set {_logicNode = value;}
	}

	public ActionNode _actionNode;
	public virtual ActionNode actionNode {
		get {return _actionNode;}
		set {_actionNode = value;}
	}
	public ProcessingScope _processingScope;
	public ProcessingScope processingScope {
		get {return _processingScope;}
		set {_processingScope = value;}
	}
	public float _inputThreshold;
	public float inputThreshold {
		get {return _inputThreshold;}
		set {_inputThreshold = value;}
	}
	public float _outputThreshold;
	public float outputThreshold {
		get {return _inputThreshold;}
		set {_inputThreshold = value;}
	}
	public IBlackBoard _blackboard;
	public IBlackBoard blackboard {
		get {return _blackboard;}
		set {_blackboard = value;}
	}

	public List<BaseNode> _lowerNodes = new List<BaseNode> ();
	public List<BaseNode> lowerNodes {
		get {return _lowerNodes;}
		set {_lowerNodes = value;}
	}
	public IUniNode _upperNode;
	public IUniNode upperNode {
		get {return _upperNode;}
		set {_upperNode = value;}
	}
	public BaseNodeAI _topNode;
	public BaseNodeAI topNode {
		get {return _topNode;}
		set {_topNode = value;}
	}
	public List<NodeConnection> _outConnections = new List<NodeConnection> ();
	public List<NodeConnection> outConnections {
		get {return _outConnections;}
		set {_outConnections = value;}
	}
	public List<NodeConnection> _inConnections = new List<NodeConnection> ();
	public List<NodeConnection> inConnections {
		get {return _inConnections;}
		set {_inConnections = value;}
	}

	public Dictionary <VarType, NodeConnection> outConnectionByVarType = new Dictionary<VarType, NodeConnection> (); 
	public Dictionary <VarType, NodeConnection> inConnectionByVarType = new Dictionary<VarType, NodeConnection> (); 

	public string nodeName;
	public Dictionary <VarType, List<string>> outConNames = new Dictionary<VarType, List<string>> (); 
	public Dictionary <VarType, List<string>> inConNames = new Dictionary<VarType, List<string>> (); 

	public virtual void InitializeNode(){
		
	}
	
	public virtual NeuTreeCB Run (IBlackBoard _blackboard)	{
		return null;
	}
	#endregion

	public virtual NodeConnection GetConnection(VarType _varType, DataDirection _direction, int _n){
		if (_direction == DataDirection.IncomeData) {
			for (int i = 0; i < inConnections.Count; i++) {
				if (inConnections [i].varType == _varType && inConnections [i].num == _n) {
					return inConnections [i];
				}
			}
			return null;
		}
		if (_direction == DataDirection.OutcomeData) {
			for (int i = 0; i < outConnections.Count; i++) {
				if (outConnections[i].varType == _varType && outConnections[i].num == _n){
					return outConnections[i];
				}
			}
			return null;
		}
		return null;
	}



}
