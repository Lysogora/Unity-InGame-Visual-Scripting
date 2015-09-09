using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VSuckingFunction : BaseElementFunction {
	public int effectVal;
	// Use this for initialization
	void Start () {
	
	}

	public override void Process (List<BaseElement> _targets)	{
		effectVal = 0;
		if (funcOwner.elProperties.ContainsKey (PropertyType.VTransfer)) {
			if(funcOwner.elProperties[PropertyType.VTransfer].val > funcOwner.elProperties[PropertyType.ExcessV].val)
				effectVal = funcOwner.elProperties [PropertyType.ExcessV].val;
			else
				effectVal = funcOwner.elProperties [PropertyType.VTransfer].val;
		}

		for (int i = 0; i < _targets.Count; i++) {
			if(_targets[i].elProperties.ContainsKey(PropertyType.V)){
				if(funcOwner.elProperties[PropertyType.TeamID].val != _targets[i].elProperties[PropertyType.TeamID].val){
					_targets[i].elProperties[PropertyType.V].val -= effectVal;
					funcOwner.elProperties[PropertyType.ExcessV].val -= effectVal;
					_targets[i].elProperties[PropertyType.AttackerNum].val = funcOwner.elProperties [PropertyType.TeamID].val;				
					HudEffectsManager.inst.ShowFlyUpInfo("+ "+effectVal.ToString(), transform.position);
					// catch enemy point

				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
