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

		blackboard = new Blackboard ();

		NeuTreeCB answer = new NeuTreeCB();
		//answer.blackboard = blackboard;
		//blackboard.Project(_blackboard);
		NeuTreeCB lowerAnswer = new NeuTreeCB ();

		float nomDist = Mathf.ClosestPowerOfTwo(_inputVar.elProperties[PropertyType.Range].val);
		bool success = false;
		float thresholld = 0.99f;
		if (upperNode != null)
			thresholld = upperNode.inputThreshold;

		//Debug.Log ("Checking distance of " + blackboard.functionStimuls[FunctionType.Movement].Keys.Count);
		//List <int> el = new List<int> (blackboard.functionStimuls[FunctionType.Movement].Keys);

		//List <IdWeight> el = new List<IdWeight> (_inputList);
		//List <IdWeight> _outputList = new List<IdWeight> ();
		_outputList.Clear ();
		for (int i = 0; i < _inputList.Count; i++) {
			_outputList.Add(new IdWeight());
			_outputList[i].id = _inputList[i].id;
		}

		for (int i = 0; i < _outputList.Count; i++) {
			float dist  = Vector3.Magnitude(ElementsManager.gameElements[_outputList[i].id].transform.position - _inputVar.transform.position) / nomDist;

			//Debug.Log("element id "+el[i].id+" didtance to element "+dist+" threshold value "+thresholld);

			if(dist <= thresholld){
				_outputList[i].weight = 1 - dist;
				success = true;
			}
		}

		//_outputList = _outputList;

//		for (int p = 0; p < blackboard.baseElementsPriority.Length; p++) {
//			Debug.Log("Distance Pririty "+blackboard.baseElementsPriority[p]);
//		}

		if(!success){
			answer.replyFire = ReplyFire.Fail;
		}
		else{
			answer.replyFire = ReplyFire.Success;
		}
//		Debug.Log ("Diastance filter answer "+answer.replyFire);
		return answer;
	}
}
