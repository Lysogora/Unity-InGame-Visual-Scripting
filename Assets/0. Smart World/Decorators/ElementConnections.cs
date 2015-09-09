using UnityEngine;
using System.Collections;

public class ElementConnections : MonoBehaviour {
	public GameObject elementGO;
	public BaseActivityElement element;
	public LineRenderer[] lines; 
	public Vector3[] endpoints; 
	public Color vExtractionColor;
	public Color vGrantingColor;

	// Use this for initialization
	void Start () {
		element = elementGO.GetComponent<BaseActivityElement> ();
		StartCoroutine (UpdateConnectionView ());
	}

	public IEnumerator UpdateConnectionView(){
		yield return new WaitForEndOfFrame ();
		int oldId = -1;
		int prevV = element.elProperties [PropertyType.V].val;
		Vector3 prevTargetPos = Vector3.zero;
		while (true) {
			if(oldId != element.elProperties [PropertyType.TeamID].val){
				oldId = element.elProperties [PropertyType.TeamID].val;
				for (int i = 0; i < lines.Length; i++) {
					lines[i].SetColors(GlobalData.inst.GetTeamVisualData(oldId).color, GlobalData.inst.GetTeamVisualData(oldId).color);
				}
			}
			for (int i = 0; i < lines.Length; i++) {
				lines[i].material.mainTextureScale = new Vector2(Vector3.Magnitude(endpoints[i] - transform.position)*4 , 1);
			}


			int newV = element.elProperties [PropertyType.V].val;
			if(newV != prevV){
				if(newV > prevV){
					int n = element.elProperties [PropertyType.TeamID].val;
					if (n < 0) n = 0; 
					HudEffectsManager.inst.ShowAttackCircles(elementGO.transform.position, 
					                                         GlobalData.inst.GetTeamVisualData(n).particleColor);
				}
				else if(newV < prevV){
					int n = element.elProperties [PropertyType.AttackerNum].val;
					if (n < 0) n = 0; 
					HudEffectsManager.inst.ShowAttackCircles(elementGO.transform.position, 
					                                         GlobalData.inst.GetTeamVisualData(n).particleColor);
				}
				prevV = newV;
			}


			yield return new WaitForSeconds(0.2f);
		}
	} 
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 sprOffset = Vector3.zero;

		for (int i = 0; i < lines.Length; i++) {
			lines[i].SetPosition(0, transform.position);
			if (element.targets.Count > i){
				endpoints[i] = element.targets[i].transform.position;
				lines[i].SetPosition(1, endpoints[i]);
			}
			else{
				endpoints[i] = transform.position;
				lines[i].SetPosition(1, endpoints[i]);
			}
			if(lines[i].isVisible){
				sprOffset = lines[i].material.mainTextureOffset;
				sprOffset.x -= 0.01f;
				lines[i].material.mainTextureOffset = sprOffset;
			}

		}
	}
}
