using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

[System.Serializable]
public class OperationNT : INTOperation<INTData<BaseElement>, IOpModNT> {
	#region INTOperation implementation
	public float weight;
	public float Weight {
		get {return weight;}
		set {weight = value;}
	}

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

	public List<IOpModNT> opMods = new List<IOpModNT>();
	public List<IOpModNT> OpMods {
		get {return opMods;}
		set {opMods = value;}
	}

	public virtual void Initialize (){
	
	}

	public virtual void ProcessData (List<INTData<BaseElement>> _subjects, List<INTData<BaseElement>> _objects)	{

	}
	#endregion
}
