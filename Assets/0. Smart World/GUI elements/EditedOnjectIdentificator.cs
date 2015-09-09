using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EditedOnjectIdentificator : MonoBehaviour, IPointerClickHandler{


	public IwPropertyValue<PropertyType, int> prop;
	public IElementFunction<BaseElement> func;

	// Use this for initialization
	void Start () {
	
	}

	public void SetIdentificator(IwPropertyValue<PropertyType, int> _prop){
		prop = _prop;
	}
	public void SetIdentificator(IElementFunction<BaseElement> _func){
		func = _func;
	}
	public IwPropertyValue<PropertyType, int> GetProp(){
		if (prop != null) {
			//Debug.Log ("PROP AVAILABLE");
			return prop;
		}
		//Debug.Log ("NO PROP"+gameObject.name);
		return null;
	}
	public IElementFunction<BaseElement> GetFunc(){
		if (func != null) {
			return func;
		}
		return null;
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData){
		Debug.Log ("CLICked on the property slot");
		for (int i = 0; i < ElementInfoManager.inst.propEditSlots.Count; i++) {
			ElementInfoManager.inst.propEditSlots[i].DeselectSlot();
		}
		for (int i = 0; i < ElementInfoManager.inst.funcEditSlots.Count; i++) {
			ElementInfoManager.inst.funcEditSlots[i].DeselectSlot();
		}
		this.gameObject.GetComponent<BaseSlot> ().SelectSlot ();

		if (prop != null && func == null) {
			ElementInfoManager.inst.EditElementCompound(prop);
		}
		if (prop == null && func != null) {
			ElementInfoManager.inst.EditElementCompound(func);
		}
	}

	#endregion
	
	// Update is called once per frame
	void Update () {
	
	}
}
