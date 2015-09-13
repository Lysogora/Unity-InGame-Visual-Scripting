using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuTree2;

public class TreeConstructor : MonoBehaviour {

	public static TreeConstructor inst;

	public TestOutput_Node node2;
	public TestOutput_Node node1;

	void Awake(){
		if (inst == null)
			inst = this;
	}

	// Use this for initialization
	void Start () {
		//TestIdWeightClass ();
	}
	//process output and input of vars and lists
	public void PassArgument(BaseNode from, BaseNode to, VarType fromType, VarType outType){
	switch (fromType) {
		case VarType.FloatVar:
			IOutputVar<float> fromInterface = from as IOutputVar<float>;
			IInputVar<float> toInterface = to as IInputVar<float>;
			if(fromInterface != null && toInterface != null){
				toInterface.inputVar = fromInterface.outputVar;
			}
			break;

		case VarType.IdWeightVar:
			IOutputVar<IdWeight> fromIdWeight = from as IOutputVar<IdWeight>;
			IInputVar<IdWeight> toIdWeight = to as IInputVar<IdWeight>;
			if(fromIdWeight != null && toIdWeight != null){
				toIdWeight.inputVar = fromIdWeight.outputVar; 
			}
			break;
		
		default:
			break;
		}
	}

	//process output and input lists of vars and lists (List<T> and List<List<T>>)

	// the function unboxes the interface of output variables in the output node(outNode) and the interface of input variables
	// in the input node (inNode). OutType and inType indicate the type of the interface. OutNum and inNum indicates the place 
	// of output and input T and List<T> in case of output or input variables interface is of kind List<T> or List<List<T>>
	// respectively.

	public void PassArgument(BaseNode outNode, BaseNode inNode, VarType outType, VarType inType, int outNum, int inNum){


		switch (outType) {
		case VarType.IdWeightList:
			IOutputList<IdWeight> outIdWeightList = outNode as IOutputList<IdWeight>;

			IInputList<IdWeight> inIdWeightList = inNode as IInputList<IdWeight>;
			IInputMultyList<IdWeight> inIdWeightMultyList = inNode as IInputMultyList<IdWeight>;
			// 1 list out - 1 list in
			if(inType == VarType.IdWeightList){
				if(outIdWeightList != null && inIdWeightList != null){
					inIdWeightList.inputList = outIdWeightList.outputList; 
				}
			}
			// 1 list out - n lists in 
			if(inType == VarType.IdWeightNestedList){
				if(outIdWeightList != null && inIdWeightMultyList != null){
					inIdWeightMultyList.inputLists[inNum] = outIdWeightList.outputList;
				}
			}
		break;
		case VarType.IdWeightNestedList:
			IOutputMultyList<IdWeight> outIdWeightMultyList = inNode as IOutputMultyList<IdWeight>;
			
			inIdWeightList = inNode as IInputList<IdWeight>;
			inIdWeightMultyList = inNode as IInputMultyList<IdWeight>;
			// n list out - 1 list in
			if(inType == VarType.IdWeightList){
				if(outIdWeightMultyList != null && inIdWeightList != null){
					inIdWeightList.inputList = outIdWeightMultyList.outputLists[outNum]; 
				}
			}
			// n list out - n lists in 
			if(inType == VarType.IdWeightNestedList){
				if(outIdWeightMultyList != null && inIdWeightMultyList != null){
					inIdWeightMultyList.inputLists[inNum] = outIdWeightMultyList.outputLists[outNum];
				}
			}
			break;

		default:
			break;
		}
	}

	public bool BindConnections(NodeConnection outNode, NodeConnection inNode, VarType outType, VarType inType, int outNum, int inNum){
		if (outType == VarType.IdWeightList) {
			Debug.Log("Binding...");
			IOutputList<IdWeight> outIdWeightList = outNode.node as IOutputList<IdWeight>;
			if(outIdWeightList == null) return false;
			Debug.Log("got output interface...");
			if(inNode.varType == VarType.IdWeightVar){
				return false;
			}
			if (inNode.varType == VarType.IdWeightList){
				Debug.Log("got list node..." + inNode.node.GetType().ToString());
				IInputList<IdWeight> inIdWeightList = inNode.node as IInputList<IdWeight>;
				if(inIdWeightList == null) return false;
				inIdWeightList.inputList = outIdWeightList.outputList; 
				if (outNode.oppositeConnections.Count == 0)
					outNode.oppositeConnections.Add(inNode);
				else{
					outNode.oppositeConnections.Clear();
					outNode.oppositeConnections.Add(inNode);
				}
				return true;
			}
			if (inNode.varType == VarType.IdWeightNestedList){
				Debug.Log("got nested list node..." + inNode.node.GetType().ToString());
				IInputMultyList<IdWeight> inIdWeightMultyList = inNode.node as IInputMultyList<IdWeight>;
				if(inIdWeightMultyList == null) return false;
				Debug.Log("got input  Nested list  interface...");
				inIdWeightMultyList.inputLists[inNum] = outIdWeightList.outputList;

				if (outNode.oppositeConnections.Count == 0){
					outNode.oppositeConnections.Add(inNode);
					Debug.Log("Adding to empty new opposite connection");
				}
				else{
					outNode.oppositeConnections.Clear();
					outNode.oppositeConnections.Add(inNode);
					Debug.Log("Replacing new opposite connection");
				}
				return true;
			}
			return false;
		}
		return false;
	}




	//TESTS
	void TestFloatStruct(){
		Debug.Log ("Testing float struct passing");
		node1 = new TestOutput_Node ();
		node1.outFloat = 1;
		
		node2 = new TestOutput_Node ();
		node2.inFloat = 0;
		
		PassArgument (node1, node2, VarType.FloatVar, VarType.FloatVar);
		
		Debug.Log ("fromNode " + node1.outFloat + " toNode " + node2.inFloat);
		Debug.Log ("changing first var");
		node1.outFloat = 2;
		Debug.Log ("fromNode " + node1.outFloat + " toNode " + node2.inFloat);
	}

	void TestIdWeightClass(){
		Debug.Log ("Testing IdWeight class passing");
		node1 = new TestOutput_Node ();
		node1.outIdWeight = new IdWeight ();
		node1.outIdWeight.weight = 0.5f;
		
		node2 = new TestOutput_Node ();
		node2.inIdWeight = new IdWeight ();
		node2.inIdWeight.weight = 0;
		
		PassArgument (node1, node2, VarType.IdWeightVar, VarType.IdWeightVar);
		
		Debug.Log ("fromNode " + node1.outIdWeight.weight + " toNode " + node2.inIdWeight.weight);
		Debug.Log ("changing first var");
		node1.outIdWeight.weight = 2.0f;
		Debug.Log ("fromNode " + node1.outIdWeight.weight + " toNode " + node2.inIdWeight.weight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
