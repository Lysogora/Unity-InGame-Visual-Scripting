///Created by Neodrop. neodrop@unity3d.ru
using UnityEngine;
using System.Collections.Generic;

public class TypeHolder : MonoBehaviour
{
    public PrimitiveType type;
}
[System.Serializable]
public class ElementRflector{
	public ElementType elementType;
	public float[] position = new float[3]{0.0f,0.0f,0.0f};
	public List<PropertyReflector> properties = new List<PropertyReflector> ();
	public List<FunctionReflector> functions = new List<FunctionReflector> ();
}

[System.Serializable]
public class PropertyReflector{
	public PropertyType propertyType;
	public int val;
	public int maxVal;

	public PropertyReflector (PropertyType _propType, int _val, int _maxVal){
		propertyType = _propType;
		val = _val;
		maxVal = _maxVal;
	}
}

[System.Serializable]
public class FunctionReflector{
	public FunctionType functionType;

	public FunctionReflector (FunctionType _funcType){
		functionType = _funcType;
	}
}