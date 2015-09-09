using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IwElement<E>: IHierarchyElement<IwElement<E>>, IElementStateProperties<System.Type, int, PropertyType, IwPropertyValue<PropertyType, int>>{
	E inst { get; }
	ElementType ElementType { get; set;}
}


//interface of an element in the hierarchy
public interface IHierarchyElement <T>{
	List <T> highElements { get; set;}
	List <T> lowElements { get; set;}
}
//interface of elements properties
public interface IElementStateProperties <K, V, P, L>{
	int id { get; set;}
	Dictionary <K, V> elStates { get; set;}
	Dictionary <P, L> elProperties { get; set;}
	List <L> propertiesList { get; set;}
}



public interface IwPropertyValue<T, V>{
	T propType { get; set;}
	V val { get; set;}
	V maxVal { get; set;}
}

//interface of elements activities which has lists of targets and functions operating with targets
public interface IElementActivity<T, F> {
	List<T> targets{ get; set;}
	Dictionary <F, IElementFunction <T>> functionsDict { get; set;}
	List <IElementFunction <T>> functions { get; set;}
	void Do();
	void AddTarget(T _target);
}
//interface of a function
public interface IElementFunction <T>{
	FunctionType functionType{ get; set;}
	T funcOwner { get; set;}
	void Process (List <T> _targets);
}

public interface IElementsBody <T>{
	List <T> elements { get; set;}
	int rating { get; set;}
	int experience { get; set;}
	int mass { get; set;}
	int speed { get; set;}

	void InitializeBody();
	void AddElement(T _parent, T _newElement);
	void DeleteElement(T _element);
	void PauseBody();
}

public interface ITypeElement <T>{
	T inst { get; }
}

public enum ElementType {BaseElement, ActivityElement};
public enum PropertyType {Empty, FirePower, V, VGenPower, TargetNum, TeamID, VStock, VTransfer, AttackerNum, ExcessV, Range, MovementSpeed, Damage, DamageRange, Radius, RotationSpeed, ownerID};
public enum FunctionType {Empty, Vgeneration, Vextraction, Vflow, Protection, Movement, VStorage, ElementSpawner, Rotation};
public enum ActionRoutineType {RotateToTarget, RotateAroundTarget};