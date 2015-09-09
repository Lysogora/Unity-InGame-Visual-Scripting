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

	public List<IUniNode> _lowerNodes = new List<IUniNode> ();
	public List<IUniNode> lowerNodes {
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

	public virtual void InitializeNode(){
		
	}
	
	public virtual NeuTreeCB Run (IBlackBoard _blackboard)	{
		return null;
	}
	#endregion



}
