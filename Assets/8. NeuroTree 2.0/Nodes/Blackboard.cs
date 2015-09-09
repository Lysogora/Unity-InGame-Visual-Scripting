using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blackboard : MonoBehaviour, IBlackBoard {
	#region IBlackBoard implementation
	public int _layer;
	public int layer {
		get {return _layer;}
		set {_layer = value;}
	}
	public List<BaseElement> _baseElements = new List<BaseElement> ();
	public List<BaseElement> baseElements {
		get {return _baseElements;}
		set {_baseElements = value;}
	}
	public float[] _baseElementsPriority;
	public float[] baseElementsPriority {
		get {return _baseElementsPriority;}
		set {_baseElementsPriority = value;}
	}
	public List<BaseActivityElement> _activeElements = new List<BaseActivityElement> ();
	public List<BaseActivityElement> activeElements {
		get {return _activeElements;}
		set {_activeElements = value;}
	}
	public float[] _activityElementsPriority;
	public float[] activityElementsPriority {
		get {return _activityElementsPriority;}
		set {_activityElementsPriority = value;}
	}
	public Dictionary<ActionNode, float> _actionsPriority = new Dictionary<ActionNode, float> ();
	public Dictionary<ActionNode, float> actionsPriority {
		get {return _actionsPriority;}
		set {_actionsPriority = value;}
	}
	public List <FunctionPower> _functions = new List<FunctionPower> ();
	public List <FunctionPower> functions { 
		get{return _functions;} 
		set{_functions = value;}
	}

	public BaseActivityElement _subject;
	public BaseActivityElement subject {
		get {return _subject;}
		set {_subject = value;}
	}

	//table od stimuls for each function of the relevant element
	public Dictionary <FunctionType, Dictionary <int, float>> _functionStimuls = new Dictionary<FunctionType, Dictionary <int, float>> ();
	public Dictionary <FunctionType, Dictionary <int, float>> functionStimuls{
		get{return _functionStimuls;}
		set{_functionStimuls = value;}
	}

	public Dictionary <FunctionType, List <IdWeight>> _dataMaps = new Dictionary<FunctionType, List<IdWeight>> ();
	public Dictionary <FunctionType, List <IdWeight>> dataMaps{
		get{return _dataMaps;}
		set{_dataMaps = value;}
	}

	public List<IdWeight> attackMap = new List<IdWeight> ();

	public List <float> processedEls = new List<float> ();


	public void Project (IBlackBoard other)	{

		Debug.Log ("Keys before");
		foreach (var item in other.functionStimuls.Keys) {
			Debug.Log("in "+item+" array there are "+other.functionStimuls[item].Keys.Count);			
		}

		functionStimuls.Clear ();
		functionStimuls = new Dictionary<FunctionType, Dictionary <int, float>> (other.functionStimuls);

		functions.Clear();
		for (int i = 0; i < other.functions.Count; i++) {
			functions.Add(new FunctionPower(other.functions[i].fType, 0.0f));
		}
		subject = other.subject;

		Debug.Log ("Keys after");
		foreach (var item in _functionStimuls.Keys) {
			Debug.Log("in "+item+" array there are "+_functionStimuls[item].Keys.Count);			
		}
	}
	public void Blend (IBlackBoard other, float share)	{
		foreach (var item in other.functionStimuls.Keys) {
			if(functionStimuls.ContainsKey(item)){
				List <int> el = new List<int> (other.functionStimuls[item].Keys);
				for (int i = 0; i < el.Count; i++) {
					if(functionStimuls[item].ContainsKey(el[i])){
						functionStimuls[item][el[i]] = other.functionStimuls[item][el[i]] * share;
					}
				}
			}
		}

		for (int i = 0; i < functions.Count; i++) {
			functions[i].val += other.functions[i].val * share;
		}
	}

	public FunctionPower GetFunction (FunctionType _fType){
		for (int i = 0; i < functions.Count; i++) {
			if(functions[i].fType == _fType) return functions[i];
		}
		return null;
	}

	#endregion

	// Use this for initialization
	void Start () {

	}
	public void InitializeBlackboard(){
		//create dictionary according to subelements functions
//		foreach (var fType in subject.functionsDict.Keys) {
//			if(!functionStimuls.ContainsKey(fType)){
//				functionStimuls.Add(fType, new Dictionary <int, float>());
//			}
//		}

		//add datamaps to dictionary
		dataMaps.Add (FunctionType.Vextraction, attackMap);
	}
	public float GetElementWeight (FunctionType _ftype, int _id){
		if (functionStimuls.ContainsKey (_ftype)) {
			if(functionStimuls[_ftype].ContainsKey(_id)){
				return functionStimuls[_ftype][_id];
			}
			else{
				return -10.0f;
			}
		}
		else{
			return -10.0f;
		}
	}
	public void SetElementWeight (FunctionType _ftype, int _id, float _weight){
		if (functionStimuls.ContainsKey (_ftype)) {
			if(functionStimuls[_ftype].ContainsKey(_id)){
				functionStimuls[_ftype][_id] = _weight;
			}
			else{
				functionStimuls[_ftype].Add(_id, _weight);
			}
		}
		else{
			functionStimuls.Add(_ftype, new Dictionary<int, float>());
			functionStimuls[_ftype].Add(_id, _weight);
		}
	}

	public void UpdateTable(List<int> _ids){
		foreach (var item in functionStimuls.Keys) {
			functionStimuls[item].Clear();
			for (int i = 0; i < _ids.Count; i++) {
				functionStimuls[item].Add(_ids[i], 0.0f);
			}

		}
		foreach (var item in dataMaps.Keys) {
			dataMaps[item].Clear();
			for (int i = 0; i < _ids.Count; i++) {
				IdWeight newIdW = new IdWeight();
				newIdW.id = _ids[i];
				dataMaps[item].Add(newIdW);
			}
		}

	
	}

	public void PrepareDictionary(){
		actionsPriority.Add (ActionNode.Attack, 0.0f);
	}

	public BaseElement GetTopElements(int num){
		BaseElement topElement = new BaseElement();
		List <int> processedIDs = new List<int> (functionStimuls [FunctionType.Movement].Keys);
		processedEls.Clear ();
		processedEls = new List<float> (functionStimuls [FunctionType.Movement].Values);
		float maxPriority = 0.0f;
		float lowerMargin = 0.0f;
		int selectedEl = -1;
		for (int i = 0; i < processedEls.Count; i++) {
			if(processedEls[i] > maxPriority){
				selectedEl = i;
				maxPriority = processedEls[i];
			}				
		}
		if (selectedEl >= 0)
			return ElementsManager.gameElements[processedIDs[selectedEl]];
		else
			return null;
	}

	public FunctionType GetTopFunctions(){
		float maxPriority = 0.0f;
		int selectedEl = -1;

		for (int i = 0; i < functions.Count; i++) {
			if(functions[i].val > maxPriority){
				selectedEl = i;
				maxPriority = functions[i].val;
			}
		}

		if (selectedEl >= 0)
			return functions [selectedEl].fType;
		else
			return FunctionType.Empty;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
