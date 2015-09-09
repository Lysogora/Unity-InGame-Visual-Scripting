using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VGenFunction : BaseElementFunction {
	
	// Use this for initialization
	void Start () {
		
	}
	public int effectVal;
	public override void Process (List<BaseElement> _targets)	{
		int effectVal = 0;
		if(funcOwner.elProperties.ContainsKey (PropertyType.VGenPower))
			effectVal = funcOwner.elProperties [PropertyType.VGenPower].val;
		funcOwner.elProperties [PropertyType.V].val += effectVal;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}