using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuTree2;

public class BaseNodeAI : MonoBehaviour {


	public Blackboard _blackboard;
	public Blackboard blackboard {
		get {return _blackboard;}
		set {_blackboard = value;}
	}
	
	public List<BaseNode> _lowerNodes = new List<BaseNode> ();
	public List<BaseNode> lowerNodes {
		get {return _lowerNodes;}
		set {_lowerNodes = value;}
	}

	public BaseActivityElement controlledEl;

	public float refreshRate;

	public AIBlock aiBlock;

	public Node_DataMap attackMap;
	public Node_DistanceFilter distanceFilter;
	public Node_BestValue bestVal;
	public Node_WeightBlend weightBlend; 
	public Node_AddTarget addTargetNode;
	public Node_OuterElementSelector outerElNode;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		//LaunchAI ();
	}

	public void LaunchAI(){
		//create test AI nodes
		aiBlock = new AIBlock ();
//		TestNodeAI ();
//		InputTestNodeAI ();
		NestedListInputTestNodeAI ();
//		SettingTargetTestNodeAI ();
		//visualize test Ai for current purposes
		VisualScriptEditor.inst.ShowAI (aiBlock);
		//Launch them
		StartCoroutine (AICoroutine());
	}

	void TestNodeAI(){
		Node_AND andNode = new Node_AND ();
		andNode.inputThreshold = 1.0f;

		Node_OuterElementSelector nmbNode = new Node_OuterElementSelector ();
		nmbNode.inputThreshold = 0.5f;

		Node_DistanceFilter distNode = new Node_DistanceFilter ();
		distNode.inputThreshold = 0.5f;

		Node_Action movementNode = new Node_Action ();
		movementNode.inputThreshold = 1.0f;
		movementNode.actionNode = ActionNode.Attack;
		movementNode.functionType = FunctionType.Movement;

		lowerNodes.Add (andNode);
		//andNode.upperNode = this;
		andNode.lowerNodes.Add (nmbNode);
		nmbNode.upperNode = andNode;
//		andNode.lowerNodes.Add (distNode);
//		distNode.upperNode = andNode;
		andNode.lowerNodes.Add (movementNode);
		movementNode.upperNode = andNode;


	}
	void InputTestNodeAI(){
		attackMap = new Node_DataMap ();
		attackMap.topNode = this;
		attackMap.InitializeNode ();

		distanceFilter = new Node_DistanceFilter ();
		distanceFilter._inputVar = blackboard.subject;
		distanceFilter._inputList = attackMap.dataMap;
		distanceFilter.inputThreshold = 0.5f;

		bestVal = new Node_BestValue ();
		bestVal.topQuantity = 1;
		bestVal._inputList = distanceFilter._outputList;

		lowerNodes.Add (distanceFilter);
		lowerNodes.Add (bestVal);
	}

	void NestedListInputTestNodeAI(){
		attackMap = new Node_DataMap ();
		attackMap.topNode = this;
		attackMap.InitializeNode ();
		
		distanceFilter = new Node_DistanceFilter ();
		distanceFilter._inputVar = blackboard.subject;
		distanceFilter._inputList = outerElNode._outputList;
		distanceFilter.inputThreshold = 0.5f;
		distanceFilter.outputThreshold = 0.5f;
		distanceFilter.InitializeNode ();

		weightBlend = new Node_WeightBlend ();
		weightBlend.InitializeNode ();
//		weightBlend._inputLists.Add (attackMap.dataMap);
//		weightBlend._inputLists.Add (distanceFilter._outputList);

//		TreeConstructor.inst.PassArgument (attackMap, weightBlend, VarType.IdWeightList, VarType.IdWeightNestedList, 0, 0);
//		TreeConstructor.inst.PassArgument (distanceFilter, weightBlend, VarType.IdWeightList, VarType.IdWeightNestedList, 0, 1);

		VarPass mapCon = attackMap.GetConnection (VarType.IdWeightList, DataDirection.OutcomeData, 0);
		VarPass distCon = distanceFilter.GetConnection (VarType.IdWeightList, DataDirection.OutcomeData, 0);
		VarPass blendCon1 = weightBlend.GetConnection (VarType.IdWeightNestedList, DataDirection.IncomeData, 0);
		VarPass blendCon2 = weightBlend.GetConnection (VarType.IdWeightNestedList, DataDirection.IncomeData, 1);

		if(mapCon == null) Debug.Log("1");
		if(distCon == null) Debug.Log("2");
		if(blendCon1 == null) Debug.Log("3");
		if(blendCon2 == null) Debug.Log("4");

		TreeConstructor.inst.BindConnections (mapCon, blendCon1, mapCon.varType, blendCon1.varType, mapCon.num, blendCon1.num);
		TreeConstructor.inst.BindConnections (distCon, blendCon2, distCon.varType, blendCon2.varType, distCon.num, blendCon2.num);
				
		lowerNodes.Add (distanceFilter);
		lowerNodes.Add (weightBlend);

		aiBlock.coreNodes = lowerNodes;
		aiBlock.dataNodes.Add (attackMap);
	}

	void SettingTargetTestNodeAI(){
		attackMap = new Node_DataMap ();
		attackMap.topNode = this;
		attackMap.InitializeNode ();

		outerElNode = new Node_OuterElementSelector ();
		outerElNode._inputList = attackMap.dataMap;
		outerElNode.topNode = this;
		outerElNode.inputThreshold = 0.5f;
		outerElNode.outputThreshold = 1.0f;
		
		distanceFilter = new Node_DistanceFilter ();
		distanceFilter._inputVar = blackboard.subject;
		distanceFilter._inputList = outerElNode._outputList;
		distanceFilter.inputThreshold = 0.5f;
		distanceFilter.outputThreshold = 0.5f;
		
		bestVal = new Node_BestValue ();
		bestVal.topQuantity = 1;
		bestVal._inputList = distanceFilter._outputList;

		addTargetNode = new Node_AddTarget ();
		addTargetNode.inputIds = bestVal._outputList;
		addTargetNode.controlledElement = blackboard.subject;

		lowerNodes.Add (outerElNode);
		lowerNodes.Add (distanceFilter);
		lowerNodes.Add (bestVal);
		lowerNodes.Add (addTargetNode);

		aiBlock.coreNodes = lowerNodes;
		aiBlock.dataNodes.Add (attackMap);
	}

	public IEnumerator AICoroutine(){

		while (true) {
			NeuTreeCB lowerAnswer = new NeuTreeCB();
			//clear blackboard
			blackboard.baseElementsPriority = new float[blackboard.baseElements.Count];
			blackboard.activityElementsPriority = new float[blackboard.activeElements.Count];
			for (int i = 0; i < blackboard.functions.Count; i++) {
				blackboard.functions[i].val = 0.0f;
			}

			for (int i = 0; i < lowerNodes.Count; i++) {
				lowerAnswer = lowerNodes[i].Run(blackboard);

			}
			// temporary call of movement function for the test
			if(blackboard.subject.functionsDict.ContainsKey(FunctionType.Movement)){
				List <BaseElement> bes = new List<BaseElement>();
				for (int i = 0; i < bestVal._outputList.Count; i++) {
					BaseElement trg = ElementsManager.inst.GetElement(bestVal._outputList[i].id);
					if(trg != null){
						bes.Add(trg);
					}
				}
				blackboard.subject.functionsDict[FunctionType.Movement].Process(bes);
			}

			yield return new WaitForSeconds (refreshRate);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
