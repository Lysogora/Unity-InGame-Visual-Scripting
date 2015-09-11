using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_OuterElementSelector : BaseNode, IInputList<IdWeight>, IOutputList<IdWeight> {

	#region IInputList implementation
	public List<IdWeight> _inputList = new List<IdWeight> ();
	List<IdWeight> IInputList<IdWeight>.inputList {
		get {return _inputList;}
		set {_inputList = value;}
	}
	#endregion

	#region IOutputList implementation
	public List<IdWeight> _outputList = new List<IdWeight> ();
	List<IdWeight> IOutputList<IdWeight>.outputList {
		get {return _outputList;}
		set {_outputList = value;}
	}	
	#endregion

	public override void InitializeNode(){

		if (outConnections.Count < 1) {
			NodeConnection connection = new NodeConnection ();
			outConnections.Add (connection);
			connection.InitializeConnection(this, DataDirection.OutcomeData, VarType.IdWeightList, 0);
		}

		if (inConnections.Count < 1) {
			NodeConnection connection = new NodeConnection ();
			outConnections.Add (connection);
			connection.InitializeConnection(this, DataDirection.IncomeData, VarType.IdWeightList, 0);
		}
	}

	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
				
		NeuTreeCB answer = new NeuTreeCB();
				
		int ownerID = topNode.blackboard.subject.elProperties[PropertyType.ownerID].val;
		bool success = false;
		float thresholld = 0.01f;
		if (upperNode != null)
			thresholld = upperNode.inputThreshold;		

		_outputList.Clear ();
		
		for (int i = 0; i < _inputList.Count; i++) {
			IdWeight newVar = new IdWeight ();
			newVar.id = _inputList[i].id;
			newVar.weight = _inputList[i].weight;
			if(newVar.weight >= 0 ){
				_outputList.Add(newVar);

				BaseElement be = ElementsManager.inst.GetElement(_inputList[i].id);
				if(be != null){
					int newID = be.elProperties[PropertyType.ownerID].val;
					if(newID != ownerID){
						success = true;
						newVar.weight += 1.0f * outputThreshold;
					}
					else if (outputThreshold == 1.0f){
						newVar.weight = -1.0f;
					}
				}
			}
		}	
		answer.replyFire = ReplyFire.Success;
		if (success)
			answer.fireVal = 1.0f;
		//		Debug.Log ("Diastance filter answer "+answer.replyFire);
		return answer;
	}
}
