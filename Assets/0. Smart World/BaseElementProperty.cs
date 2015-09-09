using UnityEngine;
using System.Collections;
[System.Serializable]
public class BaseElementProperty : MonoBehaviour, IwPropertyValue<PropertyType, int> {
	#region IwPropertyValue implementation
	public PropertyType _propType;
	public PropertyType propType {
		get {return _propType;}
		set {_propType = value;}
	}

	public int _val;
	public int val {
		get {return _val;}
		set {_val = value;}
	}

	public int _maxVal;
	public int maxVal {
		get {return _maxVal;}
		set {_maxVal = value;}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
