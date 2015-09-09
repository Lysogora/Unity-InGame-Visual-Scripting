using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementEditor : MonoBehaviour {
	public static ElementEditor inst;

	public BaseActivityElement processActivityElement;

	void Awake(){
		inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void AddProperty(PropertyType _propType){
	
	}

	public void DeleteProperty(PropertyType _propType){
	
	}

	public void AddProppertyValue(IwPropertyValue<PropertyType, int> _prop){
		_prop.maxVal += 1;
	}

	public void DecreasePropertyValue(IwPropertyValue<PropertyType, int> _prop){
		_prop.maxVal -= 1;
	}

	public void AddFunction(FunctionType _funcType){
		
	}
	
	public void DeleteFunction(FunctionType _funcType){
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
