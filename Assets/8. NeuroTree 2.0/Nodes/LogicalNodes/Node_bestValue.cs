using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_BestValue : BaseNode, IInputList<IdWeight>, IInputVar<int>, IOutputList<IdWeight> {
	#region IInputList implementation
	public List<IdWeight> _inputList = new List<IdWeight> ();
	List<IdWeight> IInputList<IdWeight>.inputList {
		get {return _inputList;}
		set {_inputList = value;}
	}
	#endregion
	
	#region IInputVar implementation
	public int topQuantity;
	int IInputVar<int>.inputVar {
		get {return topQuantity;}
		set {topQuantity = value;}
	}
	#endregion
	
	#region IOutputList implementation
	public List<IdWeight> _outputList = new List<IdWeight> ();
	List<IdWeight> IOutputList<IdWeight>.outputList {
		get {return _outputList;}
		set {_outputList = value;}
	}
	
	#endregion	
	
	public override NeuTreeCB Run (IBlackBoard _blackboard){
		_outputList.Clear ();
		int selected = -1;
		float mavVal = -1.0f;
		for (int i = 0; i < _inputList.Count; i++) {
			if(_inputList[i].weight > mavVal){
				mavVal = _inputList[i].weight;
				selected = i;
			}
		}

		if (selected >= 0) {
			_outputList.Add(_inputList[selected]);
		}

		NeuTreeCB answer = new NeuTreeCB();
		answer.replyFire = ReplyFire.Success;
		return answer;
	}
	

}
