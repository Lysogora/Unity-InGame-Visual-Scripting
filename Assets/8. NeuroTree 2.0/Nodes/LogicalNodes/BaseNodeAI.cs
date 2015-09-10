﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuTree2;

public class BaseNodeAI : MonoBehaviour {


	public Blackboard _blackboard;
	public Blackboard blackboard {
		get {return _blackboard;}
		set {_blackboard = value;}
	}
	
	public List<IUniNode> _lowerNodes = new List<IUniNode> ();
	public List<IUniNode> lowerNodes {
		get {return _lowerNodes;}
		set {_lowerNodes = value;}
	}

	public BaseActivityElement controlledEl;

	public float refreshRate;

	public Node_DataMap attackMap;
	public Node_DistanceFilter distanceFilter;
	public Node_BestValue bestVal;
	public Node_WeightBlend weightBlend; 
	public Node_AddTarget addTargetNode;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		//LaunchAI ();
	}

	public void LaunchAI(){
		//create test AI nodes
//		TestNodeAI ();
//		InputTestNodeAI ();
//		MultyListInputTestNodeAI ();
		SettingTargetTestNodeAI ();
		//Launch them
		StartCoroutine (AICoroutine());
	}

	void TestNodeAI(){
		Node_AND andNode = new Node_AND ();
		andNode.inputThreshold = 1.0f;

		NotMyBody_Node nmbNode = new NotMyBody_Node ();
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

	void MultyListInputTestNodeAI(){
		attackMap = new Node_DataMap ();
		attackMap.topNode = this;
		attackMap.InitializeNode ();
		
		distanceFilter = new Node_DistanceFilter ();
		distanceFilter._inputVar = blackboard.subject;
		distanceFilter._inputList = attackMap.dataMap;
		distanceFilter.inputThreshold = 0.5f;

		weightBlend = new Node_WeightBlend ();
		weightBlend.InitializeNode ();
//		weightBlend._inputLists.Add (attackMap.dataMap);
//		weightBlend._inputLists.Add (distanceFilter._outputList);

		TreeConstructor.inst.PassArgument (attackMap, weightBlend, VarType.IdWeightList, VarType.IdWeightMultyList, 0, 0);
		TreeConstructor.inst.PassArgument (distanceFilter, weightBlend, VarType.IdWeightList, VarType.IdWeightMultyList, 0, 1);
				
		lowerNodes.Add (distanceFilter);
		lowerNodes.Add (weightBlend);
	}

	void SettingTargetTestNodeAI(){
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

		addTargetNode = new Node_AddTarget ();
		addTargetNode.inputIds = bestVal._outputList;
		addTargetNode.controlledElement = blackboard.subject;
		
		lowerNodes.Add (distanceFilter);
		lowerNodes.Add (bestVal);
		lowerNodes.Add (addTargetNode);
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
//				blackboard.Blend(lowerAnswer.blackboard, 1.0f);

//				for (int p = 0; p < blackboard.baseElementsPriority.Length; p++) {
//					Debug.Log("Pririty "+blackboard.baseElementsPriority[p]);
//				}
			}


			yield return new WaitForSeconds (refreshRate);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
