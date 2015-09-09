using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// blends two id weight lists
[System.Serializable]
public class Node_WeightBlend : BaseNode, IOutputList<IdWeight>, IInputMultyList<IdWeight> {
	#region IOutputList implementation
	public List<IdWeight> _outputList = new List<IdWeight> ();
	List<IdWeight> IOutputList<IdWeight>.outputList {
		get {return _outputList;}
		set {_outputList = value;}
	}
	#endregion

	#region IInputMultylList implementation
	public List<List<IdWeight>> _inputLists = new List<List<IdWeight>> ();
	List<List<IdWeight>> IInputMultyList<IdWeight>.inputLists {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}
	#endregion

	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
			
		NeuTreeCB answer = new NeuTreeCB();

		bool success = false;
		float thresholld = 0.99f;
		if (upperNode != null)
			thresholld = upperNode.inputThreshold;

		_outputList.Clear ();
		if (_inputLists.Count < 2) {
			answer.replyFire = ReplyFire.Fail;
			return answer;
		}
		for (int i = 0; i < _inputLists[0].Count; i++) {
			if(_inputLists[1].Count > i){
				_outputList.Add(new IdWeight());
				_outputList[i].id = _inputLists[0][i].id;
				_outputList[i].weight = (_inputLists[0][i].weight + _inputLists[1][i].weight) / 2;
			}
		}
		answer.replyFire = ReplyFire.Success;
		return answer;
	}
}
