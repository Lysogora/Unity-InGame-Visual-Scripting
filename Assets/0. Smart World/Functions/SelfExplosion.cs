using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelfExplosion : BaseElementFunction {
	public int effectVal;
	public bool move;
	public Transform targetTrans;
	Vector3 targetVector;
	public int damage;
	// Use this for initialization
	void Start () {
		
	}
	
	public override void Process (List<BaseElement> _targets)	{
		//explosion effects

		//pass damage
		if (funcOwner.elProperties.ContainsKey (PropertyType.Damage)) {
			damage = funcOwner.elProperties[PropertyType.MovementSpeed].val / _targets.Count;
		}
		for (int i = 0; i < _targets.Count; i++) {
			if(_targets[i].elProperties.ContainsKey(PropertyType.V)){
				_targets[i].elProperties[PropertyType.V].val -= damage;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
