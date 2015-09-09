using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TestOutput_Node : BaseNode, IInputVar<float>, IOutputVar<float>,  IInputVar<IdWeight>, IOutputVar<IdWeight>{

	public float inFloat;
	float IInputVar<float>.inputVar {
		get {return inFloat;}
		set {inFloat = value;}
	}
	public float outFloat;
	float IOutputVar<float>.outputVar {
		get {return outFloat;}
		set {outFloat = value;}
	}

	public IdWeight inIdWeight;
	IdWeight IInputVar<IdWeight>.inputVar {
		get {return inIdWeight;}
		set {inIdWeight = value;}
	}
	public IdWeight outIdWeight;
	IdWeight IOutputVar<IdWeight>.outputVar {
		get {return outIdWeight;}
		set {outIdWeight = value;}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
