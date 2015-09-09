using UnityEngine;
using System.Collections;
using NeuroTree;

[System.Serializable]
public class DataNT : INTData<BaseElement> {
	#region INTData implementation
	public BaseElement objectNT;
	public BaseElement ObjectNT {
		get {return objectNT;}
		set {objectNT = value;}
	}
	public float weight;
	public float Weight {
		get {return weight;}
		set {weight = value;}
	}
	#endregion

	public DataNT(BaseElement _obj, float _weight){
		ObjectNT = _obj;
		Weight = _weight;
	}
}
