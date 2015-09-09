using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour {

	public int curentLevel;
	public bool loadLevel;
	public bool saveLevel;
	public bool passLevelsToManager;

	public List <LevelConfiguration> levels = new List<LevelConfiguration> ();

	public static LevelEditor inst;

	public LevelManager levelManager;
	public GameObject levelContainer;
	public BaseLevelManager netThugManager;
	public GlobalData globalData;
	public SaveAndLoad saveAndLoad;
	// Use this for initialization
	void Awake(){
		if (inst != null)
			inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	void LoadLevels(){

	}

	void LoadSelectedLevel(){
//		if (curentLevel >= levels.Count)
//			return;
//		LevelConfiguration newLC = levels [curentLevel];

		LevelConfiguration newLC = saveAndLoad.LoadLevelFromPrefs (curentLevel);
		if (newLC == null)
			return;

		if (levelContainer != null)
			DestroyImmediate (levelContainer);
		levelContainer = new GameObject("LEVEL ELEMENTS");

		netThugManager.teamsList.Clear ();

		Debug.Log ("Loading " + newLC.elements.Count.ToString () + " elements");

		for (int i = 0; i < newLC.elements.Count; i++) {

			switch (newLC.elements[i].elementType) {
			case ElementType.BaseElement:

				break;
			case ElementType.ActivityElement:
				GameObject newEl = Instantiate(globalData.activityElementPrefab, Vector3.zero, Quaternion.identity) as GameObject;
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

				ElementsBody eb = netThugManager.GetTeam(elTeamNum);
				if(eb != null){
					eb.elements.Add(bae);
				}
				else{
					GameObject bodyGO = new GameObject("Body "+elTeamNum);
					ElementsBody body = bodyGO.AddComponent<ElementsBody>();
					bodyGO.transform.SetParent(levelContainer.transform);
					body.id = elTeamNum;	
					netThugManager.teamsList.Add(body);
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

	void SaveSelectedLevel(){
		LevelConfiguration newLC = new LevelConfiguration ();
		newLC.levelNum = curentLevel;

		BaseActivityElement[] aEls = levelContainer.GetComponentsInChildren<BaseActivityElement> ();

		for (int i = 0; i < aEls.Length; i++) {
		
			BaseElement e = aEls[i];
			BaseActivityElement _bae = e as BaseActivityElement;
			ElementRflector el = new ElementRflector();
			newLC.elements.Add(el);

			el.elementType = e.ElementType;
			el.position = new float[3]{e.transform.position.x, e.transform.position.y, e.transform.position.z};

			IwPropertyValue<PropertyType, int>[] elProps = e.gameObject.GetComponents<IwPropertyValue<PropertyType, int>>();
			for (int p = 0; p < elProps.Length; p++) {
				IwPropertyValue<PropertyType, int> _be = elProps[p];
				PropertyReflector pr = new PropertyReflector(_be.propType, _be.val, _be.maxVal);
				el.properties.Add(pr);
			}

			if(_bae != null){
				IElementFunction<BaseElement>[] elfuncs = e.gameObject.GetComponents<IElementFunction<BaseElement>>();
				for (int f = 0; f < elfuncs.Length; f++) {
					IElementFunction<BaseElement> func = elfuncs[f];
					FunctionReflector fr = new FunctionReflector(func.functionType);
					el.functions.Add(fr);
				}
			}
			Debug.Log("element saved");
			
		}

//		if (curentLevel >= levels.Count) {
//			levels.Add (newLC);
//			newLC.levelNum = levels.Count - 1;
//			curentLevel = newLC.levelNum;
//		} else {
//			levels[curentLevel] = newLC;
//		}

		saveAndLoad.SaveLevelToPrefs (newLC);

	}

	void PassDataToDisk(){
		levelManager.levels.Clear ();
	for (int i = 0; i < 10; i++) {
			LevelConfiguration newLC = saveAndLoad.LoadLevelFromPrefs (i);
			if (newLC != null) saveAndLoad.SaveLevel(newLC);
		}
	}

	void PassDataToManager(){
		levelManager.levels.Clear ();
		for (int i = 0; i < 10; i++) {
			LevelConfiguration newLC = saveAndLoad.LoadLevelFromPrefs (i);
			if (newLC != null) levelManager.levels.Add(newLC);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (loadLevel) {
			loadLevel = false;
			LoadSelectedLevel();
		}
		if (saveLevel) {
			saveLevel = false;
			SaveSelectedLevel();
		}
		if (passLevelsToManager) {
			passLevelsToManager = false;
			PassDataToManager();
		}
	}
}
