using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ElementAction_Node : BaseNode {
	
	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
		
		blackboard = new Blackboard ();
		
		NeuTreeCB answer = new NeuTreeCB();
		answer.blackboard = blackboard;
		blackboard.Project(_blackboard);
		NeuTreeCB lowerAnswer = new NeuTreeCB ();
		
		float nomDist = Mathf.ClosestPowerOfTwo(blackboard.subject.elProperties[PropertyType.Range].val);
		bool success = false;
		float thresholld = 0.01f;
		if (upperNode != null)
			thresholld = upperNode.inputThreshold;
		
		Debug.Log ("Checking distance of " + blackboard.functionStimuls[FunctionType.Movement].Keys.Count);
		List <int> el = new List<int> (blackboard.functionStimuls[FunctionType.Movement].Keys);
		
		for (int i = 0; i < el.Count; i++) {
			float dist  = Vector3.Magnitude(ElementsManager.gameElements[el[i]].transform.position - blackboard.subject.transform.position) / nomDist;
			
			Debug.Log("element id "+el[i]+" didtance to element "+dist+" threshold value "+thresholld);
			
			if(dist <= thresholld){
				blackboard.functionStimuls[FunctionType.Movement][el[i]] = 1 - dist;
				success = true;
			}
		}
		
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
