using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeuTree2 {
	public interface IUniNode{
		LogicNode logicNode {get; set;}
		ActionNode actionNode {get; set;}
		ProcessingScope processingScope {get; set;}
		float inputThreshold { get; set;}
		float outputThreshold { get; set;}
		IBlackBoard blackboard { get; set;}
		List <BaseNode> lowerNodes { get; set;}
		BaseNode upperNode {get; set;}
		BaseNodeAI topNode { get; set;}
		void InitializeNode();
		NeuTreeCB Run(IBlackBoard _blackboard);
	}
}

public class NeuTreeCB{
	public float fireVal;
	public ReplyFire replyFire;
	public IBlackBoard blackboard;
	public List <float> weights = new List<float> ();
}

[System.Serializable]
public class IdWeight{
	public int id;
	public float weight;
}

public interface IBlackBoard{
	int layer { get; set;}
	Dictionary <FunctionType, Dictionary <int, float>> functionStimuls { get; set;}
	Dictionary <FunctionType, List <IdWeight>> dataMaps { get; set;}
	List <BaseElement> baseElements{ get; set;}
	float[] baseElementsPriority { get; set;}

	List <BaseActivityElement> activeElements{ get; set;}
	float[] activityElementsPriority { get; set;}

	Dictionary<ActionNode, float> actionsPriority { get; set;}

	List <FunctionPower> functions { get; set;}

	void Project(IBlackBoard other);
	void Blend(IBlackBoard other, float share);
	void InitializeBlackboard();

	FunctionPower GetFunction (FunctionType _fType);

	BaseActivityElement subject{ get; set;}

	float GetElementWeight (FunctionType _ftype, int _id);
	void SetElementWeight (FunctionType _ftype, int _id, float _weight);
	void UpdateTable (List<int> _ids);
}

public enum LogicNode {None, AND, OR, IF, ELSE};
public enum ActionNode {None, Attack, Wait, ChangeSubTree, BlendDecisionSpaces};
public enum ProcessingScope {None, All, Primitives, ActiveElements};
public enum ProcessingElements {Default, SubElements, Parent, AttackTargets, MoveTargets, SupportTargets, ExploitTargets};

public enum ReplyFire {None, Success, Fail};

[System.Serializable]
public class FunctionPower{
	public FunctionType fType;
	public float val;

	public FunctionPower(FunctionType functionType, float power){
		fType = functionType;
		val = power;
	}

	public void ClearVal(){
		val = 0.0f;
	}
}

public enum VarType {Any, IntVar, IntList, IntNestedList,
	FloatVar, FloatList, FloatNestedList, 
	IdWeightVar, IdWeightList, IdWeightNestedList, 
	ElementVar, ElementList, ElementNestedList,
	ActElementVar, ActElementList, ActElementNestedList
};



//INPUT OUTPUT INTERFACES
public interface IInputVar<T>{
	T inputVar { get; set;}
}

public interface IOutputVar<T>{
	T outputVar { get; set;}
}

public interface IInputList<T>{
	List <T> inputList { get; set;}
}

public interface IOutputList<T>{
	List <T> outputList { get; set;}
}

public interface IInputMultyList<T>{
	List <List<T>> inputLists { get; set;}
	int AddList(List<T> list, int n);
}

public interface IOutputMultyList<T>{
	List <List<T>> outputLists { get; set;}
	int AddList(List<T> list, int n);
}

public interface IInputMultyVar<T>{
	List <List<T>> inputVars { get; set;}
}

public interface IOutputMultyVar<T>{
	List <List<T>> outputVars { get; set;}
}

