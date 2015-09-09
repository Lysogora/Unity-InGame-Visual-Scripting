using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseElementFunction : MonoBehaviour, IElementFunction<BaseElement> {
	#region IElementFunction implementation
	public FunctionType _functionType;
	public virtual FunctionType functionType{
		get{return _functionType;}
		set{_functionType = value;}
	}

	public BaseElement _funcOwner;
	public BaseElement funcOwner{
		get {return _funcOwner;}
		set {_funcOwner = value;}
	}
	public virtual void Process (List<BaseElement> _targets){

	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
