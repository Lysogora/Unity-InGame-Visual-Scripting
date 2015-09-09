using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node_Action : BaseNode {

	public FunctionType functionType;

	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
		blackboard = new Blackboard ();

		NeuTreeCB answer = new NeuTreeCB();
		answer.blackboard = blackboard;
		blackboard.Project(_blackboard);
		NeuTreeCB lowerAnswer = new NeuTreeCB ();
		blackboard.GetFunction(functionType).val += 1.0f * inputThreshold;
		answer.replyFire = ReplyFire.Success;
		return answer;
	}
}