using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuTree2;

public class TreeConstructor : MonoBehaviour {

	public static TreeConstructor inst;

	public TestOutput_Node node2;
	public TestOutput_Node node1;

	// Use this for initialization
	void Start () {
		TestIdWeightClass ();
	}

	public void PassArgument(BaseNode from, BaseNode to, VarType vType){
	switch (vType) {
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

	//TESTS
	void TestFloatStruct(){
		Debug.Log ("Testing float struct passing");
		node1 = new TestOutput_Node ();
		node1.outFloat = 1;
		
		node2 = new TestOutput_Node ();
		node2.inFloat = 0;
		
		PassArgument (node1, node2, VarType.FloatVar);
		
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
		
		PassArgument (node1, node2, VarType.IdWeightVar);
		
		Debug.Log ("fromNode " + node1.outIdWeight.weight + " toNode " + node2.inIdWeight.weight);
		Debug.Log ("changing first var");
		node1.outIdWeight.weight = 2.0f;
		Debug.Log ("fromNode " + node1.outIdWeight.weight + " toNode " + node2.inIdWeight.weight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}