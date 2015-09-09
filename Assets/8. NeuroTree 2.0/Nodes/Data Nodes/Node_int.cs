using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Node_int : BaseNode, IOutputVar<int> {
	#region IOutputVar implementation
	public int dataInt = 0;
	int IOutputVar<int>.outputVar {
		get {return dataInt;}
		set {dataInt = value;}
	}
	#endregion
	
	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
