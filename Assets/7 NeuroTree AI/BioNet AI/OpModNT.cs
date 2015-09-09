using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class OpModNT : IOpModNT {
	#region IOpModNT implementation
	public OpModType modType;
	public OpModType ModType {
		get {return modType;}
		set {modType = value;}
	}
	public OpConstraintType constraintType;
	public OpConstraintType ConstraintType {
		get {return constraintType;}
		set {constraintType = value;}
	}
	public System.Type enumType;
	public System.Type EnumType {
		get {return enumType;}
		set {enumType = value;}
	}
	public int enumTypeVal;
	public int EnumTypeVal {
		get {return enumTypeVal;}
		set {enumTypeVal = value;}
	}
	public int intValue;
	public int IntValue {
		get {return intValue;}
		set {intValue = value;}
	}
	public float floatValue;
	public float FloatValue {
		get {return floatValue;}
		set {floatValue = value;}
	}
	public Vector3 vectorValue;
	public Vector3 VectorValue {
		get {return vectorValue;}
		set {vectorValue = value;}
	}

	#endregion



}
