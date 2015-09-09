using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//this function passes V from Vstock to friendly elements
public class VGrantingFunction : BaseElementFunction {
	public int effectVal;
	// Use this for initialization
	void Start () {
		
	}
	
	public override void Process (List<BaseElement> _targets)	{
		effectVal = 0;
		if (funcOwner.elProperties.ContainsKey (PropertyType.VTransfer)) {
			if(funcOwner.elProperties[PropertyType.VTransfer].val > funcOwner.elProperties[PropertyType.ExcessV].val)
				effectVal = funcOwner.elProperties [PropertyType.ExcessV].val / _targets.Count;
			else
				effectVal = funcOwner.elProperties [PropertyType.VTransfer].val / _targets.Count;
		}

		for (int i = 0; i < _targets.Count; i++) {
			if(_targets[i].elProperties.ContainsKey(PropertyType.V) && funcOwner.elProperties.ContainsKey(PropertyType.VStock)){
				if(funcOwner.elProperties[PropertyType.TeamID].val == _targets[i].elProperties[PropertyType.TeamID].val){
					_targets[i].elProperties[PropertyType.V].val += effectVal;
					funcOwner.elProperties[PropertyType.ExcessV].val -= effectVal;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
