using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementsManager : MonoBehaviour {
	public static ElementsManager inst;

	public static Dictionary<int, BaseElement> gameElements = new Dictionary<int, BaseElement>();
	public List <int> ids = new List<int> ();
	public List <int> bodyids = new List<int> ();


	void Awake(){
		if (inst == null) {
			inst = this;
		}
	}


	void Start () {
	
	}

	public static void AddElement(BaseElement _element){
		// add new eleent to lists
		if (_element._id < 0) {
			_element._id = ElementsManager.inst.GetNewID ();
			ElementsManager.inst.ids.Add (_element._id);
			gameElements.Add (_element._id, _element);
		} else if (_element.id > 0) {
			if (!ElementsManager.inst.ids.Contains (_element._id)) {
				ElementsManager.inst.ids.Add (_element._id);
			}
			else{
				_element._id = ElementsManager.inst.GetNewID ();
				ElementsManager.inst.ids.Add (_element._id);
				gameElements.Add (_element._id, _element);
			}
		
		}

		ElementsBody eb = _element as ElementsBody;
		if (eb != null) {
			ElementsManager.inst.bodyids.Add(eb.id);
		}
	}
	public static void RemoveElement(BaseElement _element){
		// remove new eleent to lists
		if (ElementsManager.inst.ids.Contains (_element._id)) {
			ElementsManager.inst.ids.Remove (_element._id);
		}
		if (gameElements.ContainsKey(_element._id)){
			gameElements.Remove(_element._id);
		}
		if (ElementsManager.inst.bodyids.Contains(_element._id)){
			ElementsManager.inst.bodyids.Remove(_element._id);
		}
	}

	public int GetNewID(){
		int nID = -1;
		nID = Random.Range (1, 10000);
		while (ids.Contains(nID)) {
			nID = Random.Range (1, 10000);
		}
		return nID;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
