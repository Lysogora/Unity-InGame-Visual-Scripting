using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementFunction : BaseElementFunction {
	public override FunctionType functionType{
		get{return FunctionType.Movement;}
		set{_functionType = value;}
	}

	public int effectVal;
	public bool move;
	public Transform targetTrans;
	Vector3 targetVector;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	public override void Process (List<BaseElement> _targets)	{
		effectVal = 0;
		if (funcOwner.elProperties.ContainsKey (PropertyType.MovementSpeed)) {
			speed = (float)funcOwner.elProperties[PropertyType.MovementSpeed].val/10.0f;
		}
		
		for (int i = 0; i < _targets.Count; i++) {
			targetTrans = _targets[i].transform;
		}

		move = true;
		if (targetTrans == null)
			move = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (move) {
			if(targetTrans != null){
				targetVector = targetTrans.position;
			}
			funcOwner.transform.position = Vector3.MoveTowards(funcOwner.transform.position, targetVector, speed * Time.deltaTime);
		}
	}
}
