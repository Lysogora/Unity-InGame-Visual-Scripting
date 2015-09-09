using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//base element just exhist in space, has hierarchy and posess properties

public class BaseElement: MonoBehaviour, IwElement <BaseElement> {
	public BaseElement inst {
		get { return this;}
	}

	public int _id = -1;
	public int id{
		get{return _id;}
		set{_id = value;}
	}

	public ElementType elementType;
	public ElementType ElementType{
		get{return elementType;}
		set{elementType = value;}
	}

	#region IElementProperties implementation
	public Dictionary<System.Type, int> _elStates = new Dictionary<System.Type, int> ();
	public Dictionary<System.Type, int> elStates {
		get { return _elStates;}
		set {_elStates = value;}
	}
	public Dictionary<PropertyType, IwPropertyValue<PropertyType, int>> _elProperties = new Dictionary<PropertyType,IwPropertyValue<PropertyType, int>> ();
	public Dictionary<PropertyType,IwPropertyValue<PropertyType, int>> elProperties {
		get { return _elProperties;}
		set {_elProperties = value;}
	}

	public List <IwPropertyValue<PropertyType, int>> _propertiesList = new List<IwPropertyValue<PropertyType, int>> ();
	public List <IwPropertyValue<PropertyType, int>> propertiesList{
		get{return _propertiesList;}
		set{_propertiesList = value;}
	}

	#endregion
	#region IHierarchyElement implementation
	public List<IwElement<BaseElement>> _highElements = new List<IwElement<BaseElement>> ();
	public List<IwElement<BaseElement>> highElements {
		get {return _highElements;}
		set {_highElements = value;}
	}
	public List<IwElement<BaseElement>> _lowElements = new List<IwElement<BaseElement>> ();
	public List<IwElement<BaseElement>> lowElements {
		get {return _lowElements;}
		set {_lowElements = value;}
	}

	public bool ready;
	#endregion

	// Use this for initialization
	void Start () {
		//InitializeElement ();
	}

	public virtual void InitializeElement(){
		BaseElementProperty[] props = this.gameObject.GetComponents < BaseElementProperty> ();
		for (int i = 0; i < props.Length; i++) {
			if(!elProperties.ContainsKey(props[i].propType)){
				elProperties.Add(props[i].propType, props[i]);
				propertiesList.Add(props[i]);
			}
		}
		ready = true;
	}

	public virtual void ActivateElement(){
	
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
