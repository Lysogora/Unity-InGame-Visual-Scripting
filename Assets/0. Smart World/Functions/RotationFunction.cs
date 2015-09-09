using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationFunction : BaseElementFunction {
	public override FunctionType functionType{
		get{return FunctionType.Rotation;}
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
		if (funcOwner.elProperties.ContainsKey (PropertyType.RotationSpeed)) {
			speed = (float)funcOwner.elProperties[PropertyType.RotationSpeed].val;
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
			funcOwner.transform.rotation = Quaternion.RotateTowards(funcOwner.transform.rotation, Quaternion.LookRotation(targetVector - funcOwner.transform.position), speed * Time.deltaTime);
		}
	}
}
