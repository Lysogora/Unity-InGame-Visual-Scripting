using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseActivityElement : BaseElement, IElementActivity <BaseElement, FunctionType> {
	public bool active;

	#region IElementActivity implementation
	public virtual void Do (){

	}
	public List<BaseElement> _targets = new List<BaseElement> ();
	public List<BaseElement> targets {
		get {return _targets;}
		set {_targets = value;}
	}

	public Dictionary<FunctionType, IElementFunction<BaseElement>> _functionsDict = new Dictionary<FunctionType, IElementFunction<BaseElement>> ();
	public Dictionary<FunctionType, IElementFunction<BaseElement>> functionsDict {
		get {return _functionsDict;}
		set {_functionsDict = value;}
	}

	public List<IElementFunction<BaseElement>> _functions = new List<IElementFunction<BaseElement>> ();
	public List<IElementFunction<BaseElement>> functions {
		get {return _functions;}
		set {_functions = value;}
	}
	#endregion

	public Blackboard localBB; //local blackboard

	public BaseNodeAI AI;

	public virtual void AddTarget(BaseElement _target){

		//ckeck distance to object
		float dist = Vector3.Magnitude (_target.transform.position - transform.position);
//		Debug.Log ("received target at distance "+dist);
		if (dist > 4.0f)
			return;

		if (elProperties.ContainsKey (PropertyType.TargetNum)) {
				if(targets.Count < elProperties[PropertyType.TargetNum].val){
					BaseActivityElement _newTarget = _target as BaseActivityElement;
					if (!DoesCreateCycle(this, _newTarget))
	//				if(_newTarget == null)
	//					targets.Add(_target);
	//				else if (!_newTarget.targets.Contains(this))
					{
						targets.Add(_target);
					}
					else{
						//inform about wrong connection
					}
				}
				else{
					BaseActivityElement _newTarget = _target as BaseActivityElement;
//					if(_newTarget == null){
						if (!DoesCreateCycle(this, _newTarget))
						{
						//reposition targets in the list
//							for (int i = 0; i < targets.Count-1; i++) {
//								targets[i] = targets[i+1];
//							}
//							targets[targets.Count-1] = _target;
	//					}
//						if (!_newTarget.targets.Contains(this)){
							for (int i = 0; i < targets.Count-1; i++) {
								targets[i] = targets[i+1];
							}
							targets[targets.Count-1] = _target;
//						}
					}
//				}
			}
		}
	}

	public virtual void UpdateElementState(){
	// update V property

		//constrain propeties values
		for (int i = 0; i < propertiesList.Count; i++) {
			if(propertiesList[i].val > propertiesList[i].maxVal && propertiesList[i].propType != PropertyType.ExcessV)
				propertiesList[i].val = propertiesList[i].maxVal;
		}

		float vRate = 0.0f;
		vRate = elProperties [PropertyType.V].val / elProperties [PropertyType.V].maxVal;
	}

	// Use this for initialization
	public void Start () {
		//InitializeElement ();
	}
	public override void InitializeElement(){
		base.InitializeElement ();

		//get all functions
		BaseElementFunction[] elFuncs = this.gameObject.GetComponents < BaseElementFunction> ();
		for (int i = 0; i < elFuncs.Length; i++) {
			if(!functions.Contains(elFuncs[i])){
				functions.Add(elFuncs[i]);
				elFuncs[i].funcOwner = this;
				functionsDict.Add(elFuncs[i].functionType, elFuncs[i]);
				//add function possibility to the local blackboard
				if(localBB != null)
					localBB.functions.Add(new FunctionPower(elFuncs[i].functionType, 0.0f));
			}
		}
	}

	public override void ActivateElement(){
		if (AI != null) {
			AI.LaunchAI ();
			StartCoroutine (ElementActivity ());
		}
	}

	public virtual IEnumerator ElementActivity(){
		while (true) {
			BaseElement posTarget = localBB.GetTopElements(1);
			if(posTarget != null)
				AddTarget(posTarget);
			FunctionType funcType = localBB.GetTopFunctions();
			if(functionsDict.ContainsKey(funcType))
				functionsDict[funcType].Process(targets);
			yield return new WaitForSeconds(1.0f);
		}
	}


	//This function checks whether two lelements are part of a cycle
	bool isCycle;

	List <BaseActivityElement> elementsLine = new List<BaseActivityElement> ();
	bool DoesCreateCycle(BaseActivityElement from, BaseActivityElement to){
		isCycle = false;
		elementsLine.Clear ();
		// pass two elements to the cleared list
		elementsLine.Add (from);
		elementsLine.Add (to);
		for (int i = 1; i < elementsLine.Count; i++) {
			for (int t = 0; t < elementsLine[i].targets.Count; t++) {
				BaseActivityElement _bae = elementsLine[i].targets[t] as BaseActivityElement;
				if (_bae != null && _bae != elementsLine[i]){
					//if the target of the checked element is available in the line, then we have a cycle 
					if(elementsLine.Contains(_bae)){
						isCycle = true;
//						Debug.Log ("Checked "+elementsLine.Count.ToString()+" elements and result "+isCycle.ToString());
						return true;
					}
					//othrwise we add checked lelemets to the line and chesk its tagets in the next turn
					else{
						elementsLine.Add(_bae);
					}
				}
			}
		}
		isCycle = false;
//		Debug.Log ("Checked "+elementsLine.Count.ToString()+" elements and result "+isCycle.ToString());

		return false;

	}
	void RunCycleCheck(){

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < targets.Count; i++) {
			Debug.DrawLine(transform.position, targets[i].transform.position, Color.blue);
		}
	}
}
