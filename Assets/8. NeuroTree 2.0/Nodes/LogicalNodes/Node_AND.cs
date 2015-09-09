using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_AND : BaseNode {

	public override LogicNode logicNode {
		get {return LogicNode.AND;}
		set {_logicNode = LogicNode.AND;}
	}

	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
		blackboard = new Blackboard ();

		NeuTreeCB answer = new NeuTreeCB();
		answer.blackboard = blackboard;
		blackboard.Project(_blackboard);
		NeuTreeCB lowerAnswer = new NeuTreeCB ();
		for (int i = 0; i < lowerNodes.Count; i++) {
			lowerAnswer = lowerNodes[i].Run(blackboard);
			float fv = lowerAnswer.fireVal;
			if(lowerAnswer.replyFire == ReplyFire.Fail){
				answer.replyFire = ReplyFire.Fail;
				return answer;
			}
			else{
//				for (int p = 0; p < lowerAnswer.blackboard.baseElementsPriority.Length; p++) {
//					Debug.Log("before Lower AND Pririty "+lowerAnswer.blackboard.baseElementsPriority[p]);
//				}
//				for (int p = 0; p < blackboard.baseElementsPriority.Length; p++) {
//					Debug.Log("before AND Pririty "+blackboard.baseElementsPriority[p]);
//				}
				blackboard.Blend(lowerAnswer.blackboard, 1.0f);
//				for (int p = 0; p < blackboard.baseElementsPriority.Length; p++) {
//					Debug.Log("AND Pririty "+blackboard.baseElementsPriority[p]);
//				}
			}
		}
		answer.replyFire = ReplyFire.Success;
//		Debug.Log ("AND operator answer "+answer.replyFire);
		return answer;
	}
}
