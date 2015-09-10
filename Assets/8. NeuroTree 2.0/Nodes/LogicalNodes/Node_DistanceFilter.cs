using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_DistanceFilter : BaseNode, IInputList<IdWeight>, IInputVar<BaseElement>, IOutputList<IdWeight> {
	#region IInputList implementation
	public List<IdWeight> _inputList = new List<IdWeight> ();
	List<IdWeight> IInputList<IdWeight>.inputList {
		get {return _inputList;}
		set {_inputList = value;}
	}
	#endregion

	#region IInputVar implementation
	public BaseElement _inputVar;
	BaseElement IInputVar<BaseElement>.inputVar {
		get {return _inputVar;}
		set {_inputVar = value;}
	}
	#endregion

	#region IOutputList implementation
	public List<IdWeight> _outputList = new List<IdWeight> ();
	List<IdWeight> IOutputList<IdWeight>.outputList {
		get {return _outputList;}
		set {_outputList = value;}
	}

	#endregion

		




	public override NeuTreeCB Run (IBlackBoard _blackboard)	{

		NeuTreeCB answer = new NeuTreeCB();

		float nomDist = Mathf.ClosestPowerOfTwo(_inputVar.elProperties[PropertyType.Range].val);
		bool success = false;
		float thresholld = 0.99f;
		if (upperNode != null)
			thresholld = upperNode.inputThreshold;

		_outputList.Clear ();
		for (int i = 0; i < _inputList.Count; i++) {
			_outputList.Add(new IdWeight());
			_outputList[i].id = _inputList[i].id;
			_outputList[i].weight = _inputList[i].weight;
		}

		for (int i = 0; i < _outputList.Count; i++) {
			float dist  = Vector3.Magnitude(ElementsManager.gameElements[_outputList[i].id].transform.position - _inputVar.transform.position) / nomDist;
			if (_outputList[i].weight >= 0){
				if(dist <= thresholld){
					_outputList[i].weight += ((1 - dist) * outputThreshold);
					success = true;
				}
				else if (outputThreshold == 1.0f){
					_outputList[i].weight = -1.0f;
				}
			}
		}
		if(!success){
			answer.replyFire = ReplyFire.Fail;
		}
		else{
			answer.replyFire = ReplyFire.Success;
		}
		return answer;
	}
}
