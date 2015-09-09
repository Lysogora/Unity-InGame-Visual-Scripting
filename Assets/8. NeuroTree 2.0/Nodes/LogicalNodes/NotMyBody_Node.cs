using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NotMyBody_Node : BaseNode {
	
	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
		
		blackboard = new Blackboard ();
		
		NeuTreeCB answer = new NeuTreeCB();
		answer.blackboard = blackboard;
		blackboard.Project(_blackboard);
		NeuTreeCB lowerAnswer = new NeuTreeCB ();
		
		int ownerID = blackboard.subject.elProperties[PropertyType.ownerID].val;
		bool success = false;
		float thresholld = 0.01f;
		if (upperNode != null)
			thresholld = upperNode.inputThreshold;
		

		List <int> el = new List<int> (blackboard.functionStimuls[FunctionType.Movement].Keys);
		
		for (int i = 0; i < el.Count; i++) {
			Debug.Log("outer element? "+el[i]);
			int newID = ElementsManager.gameElements[el[i]].elProperties[PropertyType.ownerID].val;
			if(newID != ownerID){
				Debug.Log("outer element "+newID);
				blackboard.functionStimuls[FunctionType.Movement][el[i]] = 1.0f;
				success = true;
			}
			else{
				blackboard.functionStimuls[FunctionType.Movement][el[i]] -= 10.0f;
			}
		}
				
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
