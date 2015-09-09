using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalData : MonoBehaviour {
	public static GlobalData inst;

	public List <ElPropertyData> propertiesData = new List<ElPropertyData> ();
	public List <ElFunctionData> functionsData = new List<ElFunctionData> ();
	public List <TeamVisualData> teamVisualDada = new List <TeamVisualData> ();


	public GameObject activityElementPrefab;


	//public Dictionary <ObjectPropertyType, ElPropertyData> propertiesData = new Dictionary<ObjectPropertyType, ElPropertyData> ();
	//public Dictionary <FunctionType, ElFunctionData> functionsData = new Dictionary<FunctionType, ElFunctionData> ();

	void Awake(){
		inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	public IDataBlock GetDataBlock(PropertyType _key){
	for (int i = 0; i < propertiesData.Count; i++) {
			if(propertiesData[i].dataKey == _key) return propertiesData[i] as IDataBlock;
		}
		return null;
	}
	public IDataBlock GetDataBlock(FunctionType _key){
		for (int i = 0; i < functionsData.Count; i++) {
			if(functionsData[i].dataKey == _key) return functionsData[i] as IDataBlock;
		}
		return null;
	}

	public TeamVisualData GetTeamVisualData(int num){
		for (int i = 0; i < teamVisualDada.Count; i++) {
			if(teamVisualDada[i].teamNum == num) return teamVisualDada[i];
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
[System.Serializable]
public class ElPropertyData: PropertyData, IKeyDataBlock<PropertyType>{
	public PropertyType _dataKey;
	public PropertyType dataKey{
		get{return _dataKey;}
		set{_dataKey = value;}
	}
}
[System.Serializable]
public class ElFunctionData: PropertyData, IKeyDataBlock<FunctionType>{
	public FunctionType _dataKey;
	public FunctionType dataKey{
		get{return _dataKey;}
		set{_dataKey = value;}
	}
}

[System.Serializable]
public class PropertyData: IDataBlock{

	public string _name;
	public string _description;
	public Sprite _icon;
	public Sprite _bigIcon;
	public Color _color;
	public Color _particleColor;

	public string name{
		get{return _name;}
		set{_name = value;}
	}
	public string description{
		get{return _description;}
		set{_description = value;}
	}
	public Sprite icon{
		get{return _icon;}
		set{_icon = value;}
	}
	public Sprite bigIcon{
		get{return _bigIcon;}
		set{_bigIcon = value;}
	}
	public Color color{
		get{return _color;}
		set{_color = value;}
	}
	public Color particleColor{
		get{return _particleColor;}
		set{_particleColor = value;}
	}
}


public interface IDataBlock{
	string name { get; set;}
	string description { get; set;}
	Sprite icon { get; set;}
	Sprite bigIcon { get; set;}
	Color color { get; set;}

}
public interface IKeyDataBlock<T>{
	T dataKey { get; set;}
}

[System.Serializable]
public class TeamVisualData{
	public int teamNum;
	public Color color;
	public Color particleColor;
	public Sprite logo;
}
