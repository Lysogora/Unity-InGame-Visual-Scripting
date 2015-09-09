using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node_PropertyCompare : BaseNode {
	
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
		for (int i = 0; i < blackboard.baseElements.Count; i++) {
			float dist  = Vector3.Magnitude(blackboard.baseElements[i].transform.position - blackboard.subject.transform.position) / nomDist;
			//			Debug.Log(blackboard.baseElements[i].gameObject.name+" "+dist+" "+thresholld);
			if(dist <= thresholld){
				blackboard.baseElementsPriority[i] = 1 - dist;
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
