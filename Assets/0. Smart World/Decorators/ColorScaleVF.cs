using UnityEngine;
using System.Collections;

public class ColorScaleVF : ElVisualForm {

	// Use this for initialization
	void Start () {
		StartCoroutine (DelayBeforeElemetReady ());
	}

	//change inner body sprite transparency, depending on the V health of an element
	public override IEnumerator VisualVHealthUpdate(){
		Color fillColor = innerBody.color;
		int curVval = 0;
		int curTeamNum = -1;
		while(true){
			if(bActEl.elProperties[PropertyType.V].val != curVval){
				curVval = bActEl.elProperties[PropertyType.V].val;
				innerBody.transform.localScale = Vector3.one * curVval / bActEl.elProperties[PropertyType.V].maxVal;
			}
			if(bActEl.elProperties[PropertyType.TeamID].val != curTeamNum){
				curTeamNum = bActEl.elProperties[PropertyType.TeamID].val;
				innerBody.color = GlobalData.inst.GetTeamVisualData(curTeamNum).color;
				outerRim.color = GlobalData.inst.GetTeamVisualData(curTeamNum).color;
			}
			yield return new WaitForSeconds(0.1f);
		}
	} 
	
	// Update is called once per frame
	void Update () {
	
	}
}
