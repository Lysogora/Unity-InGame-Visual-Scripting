using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node_Actor : BaseNode {
			
	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
		NeuTreeCB answer = new NeuTreeCB();
		answer.blackboard = blackboard;
		blackboard.Project(_blackboard);
		NeuTreeCB lowerAnswer = new NeuTreeCB ();
		for (int i = 0; i < lowerNodes.Count; i++) {
			lowerAnswer = lowerNodes[i].Run(blackboard);
			float fv = lowerAnswer.fireVal;
			if(fv < inputThreshold){
				answer.replyFire = ReplyFire.Fail;
				return answer;
			}
			else{
				blackboard.Blend(lowerAnswer.blackboard, 1.0f);
			}
		}
		answer.replyFire = ReplyFire.Success;
		return answer;
	}
}