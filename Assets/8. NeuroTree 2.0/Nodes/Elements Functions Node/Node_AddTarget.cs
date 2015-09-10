using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node_AddTarget : BaseNode, IInputList<IdWeight>, IInputVar<BaseActivityElement> {

	#region IInputList implementation
	public List<IdWeight> inputIds = new List<IdWeight> ();
	List<IdWeight> IInputList<IdWeight>.inputList {
		get {return inputIds;}
		set {inputIds = value;}
	}
	#endregion

	#region IInputVar implementation
	public BaseActivityElement controlledElement;
	BaseActivityElement IInputVar<BaseActivityElement>.inputVar {
		get {return controlledElement;}
		set {controlledElement = value;}
	}

	#endregion

	public override NeuTreeCB Run (IBlackBoard _blackboard)	{
		
		NeuTreeCB answer = new NeuTreeCB ();
		if (controlledElement == null || inputIds == null || inputIds.Count == 0) {
			answer.replyFire = ReplyFire.Fail;
			return answer;
		}

		BaseElement target = ElementsManager.inst.GetElement (inputIds [0].id);
		if (target != null) {
			controlledElement.AddTarget (target);
			answer.replyFire = ReplyFire.Success;
			return answer;
		} else {
			answer.replyFire = ReplyFire.Fail;
			return answer;
		}

	}
}
