using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class LevelManager : MonoBehaviour {

	public int levelNum;
	public List <LevelConfiguration> levels = new List<LevelConfiguration> ();
	public GameObject levelContainer;

	public static LevelManager inst;
	// Use this for initialization
	void Awake(){
		if (inst == null)
			inst = this;
	}

	void Start () {
	
	}

	public void LoadSelectedLevel(int curentLevel){
		if (curentLevel >= levels.Count)
			return;
		LevelConfiguration newLC = levels [curentLevel];
		
//		LevelConfiguration newLC = SaveAndLoad.inst.LoadLevelFromPrefs (curentLevel);
//		if (newLC == null)
//			return;

//		LevelConfiguration newLC = SaveAndLoad.inst.LoadLevel (curentLevel.ToString());
//		if (newLC == null)
//			return;
		
		if (levelContainer != null)
			DestroyImmediate (levelContainer);
		levelContainer = new GameObject("LEVEL ELEMENTS");
		
		BaseLevelManager.inst.teamsList.Clear ();
		
		Debug.Log ("Loading " + newLC.elements.Count.ToString () + " elements");
		
		for (int i = 0; i < newLC.elements.Count; i++) {
			
			switch (newLC.elements[i].elementType) {
			case ElementType.BaseElement:
				
				break;
			case ElementType.ActivityElement:
				GameObject newEl = Instantiate(GlobalData.inst.activityElementPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				BaseActivityElement bae = newEl.GetComponent<BaseActivityElement>();
				newEl.transform.SetParent(levelContainer.transform);
				
				newEl.transform.position = new Vector3(newLC.elements[i].position[0], newLC.elements[i].position[1], newLC.elements[i].position[2]);
				
				int elTeamNum = -1;
				
				IwPropertyValue<PropertyType, int>[] elProps = bae.gameObject.GetComponents<IwPropertyValue<PropertyType, int>>();
				for (int p = 0; p < elProps.Length; p++) {
					for (int pl = 0; pl < newLC.elements[i].properties.Count; pl++) {
						if(elProps[p].propType == newLC.elements[i].properties[pl].propertyType){
							elProps[p].val = newLC.elements[i].properties[pl].val;
							elProps[p].maxVal = newLC.elements[i].properties[pl].maxVal;
							if(elProps[p].propType == PropertyType.TeamID){
								elTeamNum = elProps[p].val;
							}
						}
					}
				}
				
				IElementFunction<BaseElement>[] elfuncs = bae.gameObject.GetComponents<IElementFunction<BaseElement>>();
				//				for (int p = 0; p < elProps.Length; p++) {
				//					for (int pl = 0; pl < newLC.elements[i].properties.Count; pl++) {
				//						if(elProps[p].propType == newLC.elements[i].properties[pl].propertyType){
				//							elProps[p].val = newLC.elements[i].properties[pl].val;
				//							elProps[p].maxVal = newLC.elements[i].properties[pl].maxVal;
				//						}
				//					}
				//				}
				
				// create new body if element belongs to the new team
				
				ElementsBody eb = BaseLevelManager.inst.GetTeam(elTeamNum);
				if(eb != null){
					eb.elements.Add(bae);
				}
				else{
					GameObject bodyGO = new GameObject("Body "+elTeamNum);
					ElementsBody body = bodyGO.AddComponent<ElementsBody>();
					bodyGO.transform.SetParent(levelContainer.transform);
					body.id = elTeamNum;	
					BaseLevelManager.inst.teamsList.Add(body);
					body.elements.Add(bae);
					
					if(body.id > 1){
						BioNetAI ai = bodyGO.AddComponent<BioNetAI>();
						ai.controlledBody = body;
					}
					
				}				
				break;
			default:
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
