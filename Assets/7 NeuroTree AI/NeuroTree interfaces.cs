using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeuroTree {

	public interface INTNodeNet <NodeT> {
		List <NodeT> Nodes { get; set;}
	}


	public enum LogicOperator {And, Or}

	public interface INTNode <dataT, OperationT>{
		LogicOperator LogicOperator { get; set;}
		float Weight { get; set;}
		INTNode <dataT, OperationT> HigherNode { get; set;}
		List <INTNode <dataT, OperationT>> LowerNodes { get; set;}
		List <OperationT> Operations { get; set;}
		List <dataT> Datum { get; set;}

		void Initialize();
		void ProcessData(List <dataT> _subjects, List <dataT> _objects);

	}

	public interface INTOperation<T, Mod>{
		float Weight { get; set;}
		List <Mod> OpMods { get; set;}
		void Initialize();
		void ProcessData(List <T> _subjects, List <T> _objects);

		OpModType ModType{ get; set;}
		OpConstraintType ConstraintType { get; set;}
		System.Type EnumType { get; set; }
		int EnumTypeVal {get; set;}
		int IntValue{ get; set;}
		float FloatValue{ get; set;}
		Vector3 VectorValue { get; set;}
	}

	public interface INTData <T>{
		T ObjectNT { get; set;}
		float Weight { get; set;}
	}

	public enum OpModType {LessVal, OverVal, closeToVal, ExactVal, ExactState, NotValue}
	public enum OpConstraintType {IntC, FloatC, Vector3C, EnumC}

	public interface IOpModNT{
		OpModType ModType{ get; set;}
		OpConstraintType ConstraintType { get; set;}
		System.Type EnumType { get; set; }
		int EnumTypeVal {get; set;}
		int IntValue{ get; set;}
		float FloatValue{ get; set;}
		Vector3 VectorValue { get; set;}
		

	}

}