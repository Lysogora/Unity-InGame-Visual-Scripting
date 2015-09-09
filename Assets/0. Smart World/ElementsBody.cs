using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementsBody : BaseActivityElement, IElementsBody<BaseElement> {

	#region IElementsBody implementation
	public List <BaseElement> _elements = new List <BaseElement> ();
	public List <BaseElement> elements{
		get{return _elements;}
		set{_elements = value;}
	}


	public void AddElement (BaseElement _parent, BaseElement _newElement)	{

	}
	public void DeleteElement (BaseElement _element)	{

	}
	public void PauseBody (){

	}
	public int _rating;
	public int rating {
		get {return _rating;}
		set {_rating = value;}
	}
	public int _experience;
	public int experience {
		get {return _experience;}
		set {_experience = value;}
	}
	public int _mass;
	public int mass {
		get {return _mass;}
		set {_mass = value;}
	}
	public int _speed;
	public int speed {
		get {return _speed;}
		set {_speed = value;}
	}
	#endregion

	// Use this for initialization
	public IEnumerator Start () {
		yield return new WaitForSeconds (0.5f);
//		InitializeElement ();
	}

	// Use this for initialization
	public override void InitializeElement(){
		//initialize this object as a compound entity
		InitializeBody ();
		
		base.InitializeElement ();
		elProperties [PropertyType.ownerID].val = id;
		for (int i = 0; i < elements.Count; i++) {
			elements [i].elProperties [PropertyType.ownerID].val = elProperties [PropertyType.ownerID].val;
		}
		//get all functions
		BaseElementFunction[] elFuncs = this.gameObject.GetComponents < BaseElementFunction> ();
		for (int i = 0; i < elFuncs.Length; i++) {
			if(!functions.Contains(elFuncs[i])){
				functions.Add(elFuncs[i]);
				elFuncs[i].funcOwner = this;
				functionsDict.Add(elFuncs[i].functionType, elFuncs[i]);
//				if(localBB != null)
//					localBB.functions.Add(new FunctionPower(elFuncs[i].functionType, 0.0f));
			}
		}

		//check whether body functions are supported by its lower elements and calculate some properties of the base of sub elements properties
		//define  entites movement possibilities
		bool canMove = false;
		float totSpeed = 0;
		float rotSpeed = 0;
		if (functionsDict.ContainsKey (FunctionType.Movement)) {
			for (int i = 0; i < elements.Count; i++) {
				if(elements[i].elProperties.ContainsKey(PropertyType.MovementSpeed)){
					canMove = true;
					totSpeed += elements[i].elProperties[PropertyType.MovementSpeed].val;
				}
				if(elements[i].elProperties.ContainsKey(PropertyType.RotationSpeed)){
					canMove = true;
					rotSpeed += elements[i].elProperties[PropertyType.RotationSpeed].val;
				}
			}
		}

		localBB.functionStimuls.Add(FunctionType.Movement, new Dictionary<int, float> ());

		if(elProperties.ContainsKey(PropertyType.MovementSpeed)) {
			elProperties[PropertyType.MovementSpeed].maxVal = (int)totSpeed;
			elProperties[PropertyType.MovementSpeed].val = (int)totSpeed;
		}
		if(elProperties.ContainsKey(PropertyType.RotationSpeed)) {
			elProperties[PropertyType.RotationSpeed].maxVal = (int)rotSpeed;
			elProperties[PropertyType.RotationSpeed].val = (int)rotSpeed;
		}	
		//initialize blackBoard
		localBB.InitializeBlackboard ();
	}
	public void InitializeBody ()	{

		//launch all elements functions
		for (int i = 0; i < elements.Count; i++) {
			lowElements.Add(elements[i]);
			elements[i].InitializeElement();
//			BaseActivityElement bae = (BaseActivityElement)elements[i];
//			if(bae != null){
//				bae.Do();
//			}
		}
	}

	public override void ActivateElement(){
		if (AI != null) {
			AI.LaunchAI ();
			StartCoroutine (ElementActivity ());
		}	
	}

	//coroutine of basic activity
	public virtual IEnumerator ElementActionCoroutine(){
		active = true;
		while (true) {
			if(active){
				//PreUpdateElementState();
				for (int i = 0; i < functions.Count; i++) {
					functions[i].Process(targets);
				}
			}
			UpdateElementState();
			yield return new WaitForSeconds (0.5f);
		}
	}
	
	public virtual void AddTarget(BaseElement _target){		
		//ckeck distance to object
		float dist = Vector3.Magnitude (_target.transform.position - transform.position);
		
		if (elProperties.ContainsKey (PropertyType.TargetNum)) {
			if(targets.Count < elProperties[PropertyType.TargetNum].val){
				BaseActivityElement _newTarget = _target as BaseActivityElement;
					targets.Add(_target);
			}
			else{
				BaseActivityElement _newTarget = _target as BaseActivityElement;
			if(_newTarget == null){								
					for (int i = 0; i < targets.Count-1; i++) {
						targets[i] = targets[i+1];
					}
					targets[targets.Count-1] = _target;
					}
			}
		}
	}

	public virtual void UpdateElementState(){

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

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < targets.Count; i++) {
			Debug.DrawLine(transform.position, targets[i].transform.position, Color.blue);
		}
	}
}
