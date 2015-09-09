///Created by Neodrop. neodrop@unity3d.ru
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NeuroTree;


//[XmlIgnore]
public class SaveAndLoad : MonoBehaviour
{
	public static SaveAndLoad inst;
    public string fileName = "SaveMe.neo";
    Rect redRect, greenRect, blueRect, resetRect, loadRect;


    void Awake()
    {
		if (inst == null)
			inst = this;
		//check if necessary save directories exhist
		string[] directories = Directory.GetDirectories (Application.dataPath);
		Debug.Log (Application.dataPath);
		bool hasGoalDirectory = false;
		bool hasPlayerDirectory = false;
		for (int i = 0; i < directories.Length; i++) {
			Debug.Log ("Directory "+directories[i]);
			if(directories[i] == Application.dataPath+"/Levels Data") hasGoalDirectory = true;
			if(directories[i] == Application.dataPath+"/Players Data") hasPlayerDirectory = true;
		}
		if (!hasGoalDirectory){
			Directory.CreateDirectory (Application.dataPath+"/Levels Data");
			Debug.Log("Goals data directory is created");
		}
		else{
			//load goals data
			Debug.Log("Goals have been loaded");
		}
		if (!hasPlayerDirectory){
			Directory.CreateDirectory (Application.dataPath+"/Players Data");
			Debug.Log("Players data directory is created");
		}
		else{
			//load goals data
			Debug.Log("Players have been loaded");
		}


        redRect = new Rect(10, 10, 100, 30);
        greenRect = new Rect(redRect.x + redRect.width + 10, redRect.y, redRect.width, redRect.height);
        blueRect = new Rect(greenRect.x + greenRect.width + 10, redRect.y, redRect.width, redRect.height);
        resetRect = new Rect(blueRect.x + blueRect.width + 10, redRect.y, redRect.width, redRect.height);
        loadRect = new Rect(resetRect.x + resetRect.width + 10, redRect.y, redRect.width, redRect.height);
    }


    GameObject go = null;


	public string PlayerReflectorToString(LevelConfiguration t){
		Hashtable toSave = new Hashtable();
		toSave.Add ("PlayerData", t);	
		
		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream memoryStream = new MemoryStream())
		{
			formatter.Serialize(memoryStream, toSave);
			
			byte[] DataToSave  = memoryStream.ToArray();
			string dataString = System.Convert.ToBase64String(DataToSave);
			return dataString;
		}
	}

	public LevelConfiguration PlayerReflectorFromString(string s){
		string dataString = s;

		byte[] DataToSave = System.Convert.FromBase64String(dataString);
		using (MemoryStream memoryStream = new MemoryStream(DataToSave))
		{
			BinaryFormatter bf = new BinaryFormatter();
			Hashtable toLoad = (Hashtable)bf.Deserialize(memoryStream);
			ICollection coll = toLoad.Values;
			if(coll.Count > 0){
				foreach (LevelConfiguration obj in coll)
				{
					return obj;
				}
				//GameManager.inst.teamsInBattle[0] = coll["PlayerData"];
			}
		}
		return null;	
	}
	// save load operations with goals	
	public void DeleteLevelFile(LevelConfiguration gl){
		Debug.Log ("Fille to delete: "+Application.dataPath + "/Levels Data/" + gl.levelNum);
		File.Delete (Application.dataPath + "/Levels Data/" + gl.levelNum+".neo");
	
	}
	public void SaveLevel(LevelConfiguration gl){
		Hashtable toSave = new Hashtable();
		toSave.Add ("LevelData",gl);	
		
		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream memoryStream = new MemoryStream())
		{
			formatter.Serialize(memoryStream, toSave);
			BinarySaver.SaveLevelConfiguration(toSave, gl.levelNum.ToString());
		}
	}
	public LevelConfiguration LoadLevel(string p_id){
		string[] filenames = Directory.GetFiles (Application.dataPath+"/Levels Data");
		for (int i = 0; i < filenames.Length; i++) {
			Debug.Log("Filename: "+filenames[i]);
			if (filenames [i].Contains(p_id+".neo") && !filenames [i].Contains("meta")) {
				Hashtable toLoad = BinarySaver.Load (filenames [i]) as Hashtable;	
				ICollection coll = toLoad.Values;
				if (coll.Count > 0) {
					foreach (LevelConfiguration obj in coll) {
						return obj;
						//BaseAgent testAgent = new BaseAgent ();
						//obj.LoadBehaviourDataToAgent (testAgent);
					}
				}
			}	
		}
		return null;
	}
	public List<LevelConfiguration> LoadLevels(){
		string[] filenames = Directory.GetFiles (Application.dataPath+"/Levels Data");
		List<LevelConfiguration> mg = new List<LevelConfiguration> ();
		for (int i = 0; i < filenames.Length; i++) {
			if (!filenames [i].Contains ("meta")) {
				Hashtable toLoad = BinarySaver.Load (filenames [i]) as Hashtable;	
				ICollection coll = toLoad.Values;
				if (coll.Count > 0) {
					foreach (LevelConfiguration obj in coll) {
						Debug.Log ("LOADED NEW GOAL");
						mg.Add (obj);
					}
				}
			}
		}
		return mg;
	}
	public LevelConfiguration LoadLevelFromDisk(int n){
		string dataString = PlayerPrefs.GetString ("LevelData"+n.ToString(), "");
		if (dataString != ""){
			byte[] DataToSave = System.Convert.FromBase64String(dataString);
			using (MemoryStream memoryStream = new MemoryStream(DataToSave))
			{
				BinaryFormatter bf = new BinaryFormatter();
				Hashtable toLoad = (Hashtable)bf.Deserialize(memoryStream);
				ICollection coll = toLoad.Values;
				if(coll.Count > 0){
					foreach (LevelConfiguration obj in coll)
					{
						LevelConfiguration _lc = obj;
						return _lc;
					}
					//GameManager.inst.teamsInBattle[0] = coll["PlayerData"];
				}
			}
			
			
		}
		else{
			Debug.Log ("No saved data availanle");
		}
		return null;
	}

	//OPERATIONS WITH LEVEL DATA SAVED IN PLAYER PREFS
	public void SaveLevelToPrefs(LevelConfiguration _lc){
		Hashtable toSave = new Hashtable();
		toSave.Add ("LevelData",_lc);	
		
		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream memoryStream = new MemoryStream())
		{
			formatter.Serialize(memoryStream, toSave);
			
			byte[] DataToSave  = memoryStream.ToArray();
			string dataString = System.Convert.ToBase64String(DataToSave);
			PlayerPrefs.SetString("LevelData"+_lc.levelNum.ToString(), dataString);
		}
	}

	public LevelConfiguration LoadLevelFromPrefs(int n){
		string dataString = PlayerPrefs.GetString ("LevelData"+n.ToString(), "");
		if (dataString != ""){
			byte[] DataToSave = System.Convert.FromBase64String(dataString);
			using (MemoryStream memoryStream = new MemoryStream(DataToSave))
			{
				BinaryFormatter bf = new BinaryFormatter();
				Hashtable toLoad = (Hashtable)bf.Deserialize(memoryStream);
				ICollection coll = toLoad.Values;
				if(coll.Count > 0){
					foreach (LevelConfiguration obj in coll)
					{
						LevelConfiguration _lc = obj;
						return _lc;
					}
					//GameManager.inst.teamsInBattle[0] = coll["PlayerData"];
				}
			}
			
			
		}
		else{
			Debug.Log ("No saved data availanle");
		}
		return null;
	}

	public void EraseLevelDataFromPrefs(int n){
		PlayerPrefs.SetString("LevelData"+n.ToString(), "");
	}


	//SAVE LOAD OPERATIONS WITH PLAYERS
	public void SaveLevel2(LevelConfiguration p_ref){
		Hashtable toSave = new Hashtable();
		toSave.Add ("LevelData",p_ref);	
		
		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream memoryStream = new MemoryStream())
		{
			formatter.Serialize(memoryStream, toSave);
			BinarySaver.SavePlayer(toSave, p_ref.levelNum.ToString());
		}
	}
	


	//SAVE LOAD OPERATIONS WITH PLAYERS AT PLAYER PREFS
	public void SavePlayerAtPlayerPrefs(LevelConfiguration p_ref){
		Hashtable toSave = new Hashtable();
		toSave.Add ("PlayerData",p_ref);	
		
		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream memoryStream = new MemoryStream())
		{
			formatter.Serialize(memoryStream, toSave);
			
			byte[] DataToSave  = memoryStream.ToArray();
			string dataString = System.Convert.ToBase64String(DataToSave);
			PlayerPrefs.SetString("PlayerData", dataString);
		}
	}
	
	public LevelConfiguration LoadPlayerAtPlayerPrefs(string p_id){
		string dataString = PlayerPrefs.GetString ("PlayerData", "");
		if (dataString != ""){
			byte[] DataToSave = System.Convert.FromBase64String(dataString);
			using (MemoryStream memoryStream = new MemoryStream(DataToSave))
			{
				BinaryFormatter bf = new BinaryFormatter();
				Hashtable toLoad = (Hashtable)bf.Deserialize(memoryStream);
				ICollection coll = toLoad.Values;
				if(coll.Count > 0){
					foreach (LevelConfiguration obj in coll)
					{
						LevelConfiguration pr = obj;
						return pr;
					}
					//GameManager.inst.teamsInBattle[0] = coll["PlayerData"];
				}
			}
			
			
		}
		else{
			Debug.Log ("No saved data availanle");
		}
		return null;
	}


	public void SaveTechnicalData(){
	
	}
	public void LoadTechnicalData(){
//		string[] filenames = Directory.GetFiles (Application.dataPath);
//		for (int i = 0; i < filenames.Length; i++) {
//			Debug.Log("Filename: "+filenames[i]);
//			if (filenames [i].Contains("GameTechData.neo")) {
//				Hashtable toLoad = BinarySaver.Load (filenames [i]) as Hashtable;	
//				ICollection coll = toLoad.Values;
//				if (coll.Count > 0) {
//					foreach (GameTechData obj in coll) {
//						Debug.Log(previous)
//						//BaseAgent testAgent = new BaseAgent ();
//						//obj.LoadBehaviourDataToAgent (testAgent);
//					}
//				}
//			}	
//		}
	}



	public void SaveUserData(){
		Hashtable toSave = new Hashtable();
		//toSave.Add ("PlayerData", GameManager.inst.teamsInBattle[0]);	

		BinaryFormatter formatter = new BinaryFormatter();
		using (MemoryStream memoryStream = new MemoryStream())
		{
			formatter.Serialize(memoryStream, toSave);

			byte[] DataToSave  = memoryStream.ToArray();
			string dataString = System.Convert.ToBase64String(DataToSave);
			PlayerPrefs.SetString("PlayerData", dataString);
		}


		//byte[] DataToSave = System.conver

		//BinarySaver.Save(toSave, "PlayerData");
	}

	public void LoadUserData(){
		//Hashtable toLoad = BinarySaver.Load("PlayerData") as Hashtable;
		string dataString = PlayerPrefs.GetString ("PlayerData", "");
		if (dataString != ""){
			byte[] DataToSave = System.Convert.FromBase64String(dataString);
				using (MemoryStream memoryStream = new MemoryStream(DataToSave))
				{
					BinaryFormatter bf = new BinaryFormatter();
					Hashtable toLoad = (Hashtable)bf.Deserialize(memoryStream);
					ICollection coll = toLoad.Values;
				if(coll.Count > 0){
					foreach (LevelConfiguration obj in coll)
					{
						//GameManager.inst.teamsInBattle[0] = obj;
					}
					//GameManager.inst.teamsInBattle[0] = coll["PlayerData"];
				}
			}
				

			}
		else{
			Debug.Log ("No saved data availanle");
			}
	}

	public void SaveGame(string fName){
//		StrategyMapObject[] sceneObjs = GameObject.FindObjectsOfType<StrategyMapObject>();
//		Hashtable toSave = new Hashtable();
//		int count = objects.Count;
//		Debug.Log("Total Object count "+sceneObjs.Length);
//		for (int i = 0; i < sceneObjs.Length; i++)
//		{
//			if(sceneObjs[i] != null){
//				GameObject obj = sceneObjs[i].gameObject;	           
//				TeamData td = new TeamData();
//				td = (TeamData)GameManager.inst.strategicGameElementDict[sceneObjs[i].objID];
//				HeroParams hp = obj.GetComponent(typeof(HeroParams)) as HeroParams;
//				if (toSave.ContainsKey(obj.name)) obj.name += obj.GetInstanceID(); 
//				toSave.Add(obj.name, new ObjectSaver(obj, td, sceneObjs[i].objType, sceneObjs[i].objID, hp));
//			}
//		}
//		
//		for (int i = 0; i < count; i++) Destroy(objects[i]);
//		objects.Clear();
		Hashtable toSave = new Hashtable();

		
		BinarySaver.Save(toSave, fName);
	}

	public bool LoadGame(string fName){

//		Hashtable toLoad = BinarySaver.Load(fName) as Hashtable;
//		if (toLoad == null)
//		{
//			Debug.Log("No File Found");
//			return false;
//		}
//		ICollection coll = toLoad.Values;
//		
//		foreach (ObjectSaver obj in coll)
//		{
//			GameObject g = new GameObject();
//			switch(obj.objType){
//				case StrategyMapObjType.pirates:{
//					g = (GameObject)Instantiate(GlobalInfo.instance.stmapPiratePrefab);
//					break;
//				}
//				case StrategyMapObjType.player:{
//					g = (GameObject)Instantiate(GlobalInfo.instance.stmapPlayerPrefab);
//					break;
//				}
//			}
//			//Set unique id to the created object
//			g.GetComponent<StrategyMapObject>().objID = GameManager.GetID(g, obj.objID);
//			//GameObject g = GameObject.CreatePrimitive(obj.GetObjectType());
//			g.transform.position = obj.GetPosition();
//			//g.renderer.material.color = obj.GetColor();
//			g.name = obj.objectName;
//			HeroParams hp = g.GetComponent(typeof(HeroParams)) as HeroParams;
//			if(hp == null) hp = g.AddComponent(typeof(HeroParams)) as HeroParams;
//			if(obj.hp != null){ 
//				hp.name = obj.hp.name;
//				hp.experience = obj.hp.experience;
//			}
//			
//			//RESTORE TEAM DATA
//			TeamData td = new TeamData();
//			GameManager.inst.strategicGameElementDict.Add(obj.objID, td);
//			GameManager.inst.strategicGameElementsList.Add(td);
//			g.GetComponent<StrategyMapObject>().gameElementScript = td;
//
//			td.teamGroups = new List<GroupData>();
//			Debug.Log("Total groups loaded "+obj.groupData.Length);
//			for (int i = 0; i < obj.groupData.Length; i++) {
//				td.teamGroups.Add(obj.groupData[i]);
//			}
//		}

		return true;
	}

	public bool LoadPreBattleGame(string fName){
//		Hashtable toLoad = BinarySaver.Load(fName) as Hashtable;
//		if (toLoad == null)
//		{
//			Debug.Log("No File Found");
//			return false;
//		}
//		ICollection coll = toLoad.Values;
//		
//		foreach (ObjectSaver obj in coll) {
//			if (obj.objType != StrategyMapObjType.player) {
//				GameObject g = new GameObject ();
//				switch (obj.objType) {
//				case StrategyMapObjType.pirates:
//					{
//						g = (GameObject)Instantiate (GlobalInfo.instance.stmapPiratePrefab);
//						break;
//					}
//				case StrategyMapObjType.player:
//					{
//						g = (GameObject)Instantiate (GlobalInfo.instance.stmapPlayerPrefab);
//						break;
//					}
//				}
//				g.GetComponent<StrategyMapObject> ().objID = GameManager.GetID (g, obj.objID);
//				//GameObject g = GameObject.CreatePrimitive(obj.GetObjectType());
//				g.transform.position = obj.GetPosition ();
//				//g.renderer.material.color = obj.GetColor();
//				g.name = obj.objectName;
//				HeroParams hp = g.GetComponent (typeof(HeroParams)) as HeroParams;
//				if (hp == null)
//						hp = g.AddComponent (typeof(HeroParams)) as HeroParams;
//				if (obj.hp != null) { 
//						hp.name = obj.hp.name;
//						hp.experience = obj.hp.experience;
//				}
//
//				//RESTORE TEAM DATA
//				TeamData td = new TeamData();
//				GameManager.inst.strategicGameElementDict.Add(obj.objID, td);
//				GameManager.inst.strategicGameElementsList.Add(td);
//				g.GetComponent<StrategyMapObject>().gameElementScript = td;				
//
//				td.teamGroups = new List<GroupData> ();
//				Debug.Log ("Total groups loaded " + obj.groupData.Length);
//				for (int i = 0; i < obj.groupData.Length; i++) {
//						td.teamGroups.Add (obj.groupData [i]);
//				}
//			}
//		}
		return true;
	}

    void OnGUI()
    {
        bool created = false;


//        if (GUI.Button(resetRect, "SAVE"))
//        {
//			SaveGame("SaveMe.neo");
//        }
//
//        if (GUI.Button(loadRect, "LOAD"))
//        {
//			bool m = LoadGame("SaveMe.neo");
//        }
    }

	public void LoadDefaultGame(){
		bool m = LoadGame("SaveMe.neo");
	}
}



//[System.Serializable]
//public class ObjectSaver
//{
//	[System.Serializable]
//	public class HeroParamsSL{
//		public string name;
//		public int experience;
//		public int level;
//	}
//
//    /// <summary>
//    /// Сериализация Цвета
//    /// </summary>
//    [System.Serializable]
//    public class ColorSL 
//    {
//        float R, G, B, A;
//        public ColorSL(UnityEngine.Color color)
//        {
//            R = color.r;
//            G = color.g;
//            B = color.b;
//            A = color.a;
//        }
//
//        public Color GetColor()
//        {
//            return new Color(R, G, B, A);
//        }
//    }
//
//    [System.Serializable]
//    public class VectorSL
//    {
//        float X, Y, Z;
//
//        public VectorSL(Vector3 pos)
//        {
//            X = pos.x;
//            Y = pos.y;
//            Z = pos.z;
//        }
//
//        public Vector3 GetPosition()
//        {
//            return new Vector3(X, Y, Z);
//        }
//    }
//
////===========================================SAVE AND LOAD======================================
//    string objectType;
//	public StrategyMapObjType objType;
//	public int objID;
//    ColorSL color;
//    VectorSL position;
//	public HeroParamsSL hp;
//	public GroupData[] groupData;
//    public string objectName;
//
//    public ObjectSaver(GameObject obj, TeamData tData, StrategyMapObjType ot, int oid, HeroParams hParam = null)
//    {
//		objType = ot;
//		objID = oid;
//		//Groups Data
//		groupData = new GroupData[tData.teamGroups.Count];
//		for (int i = 0; i < tData.teamGroups.Count; i++) {
//			groupData[i] = tData.teamGroups[i];
//		}
//		if (hParam != null) {
//			hp = new HeroParamsSL();
//			hp.name = hParam.name;
//			hp.experience = hParam.experience;
//			hp.level = hParam.level;
//		}
//		//Debug.Log ("saved "+groupData.Length+" groups");
//        //color = new ColorSL(obj.renderer.material.color);
//        position = new VectorSL(obj.transform.position);
//        objectName = obj.name;
//    }
//
//    public PrimitiveType GetObjectType()
//    {
//        switch (objectType)
//        {
//            case "Capsule" :
//                return PrimitiveType.Capsule;
//            case "Cube" :
//                return PrimitiveType.Cube;
//            case "Cylinder" :
//                return PrimitiveType.Cylinder;
//        }
//
//        return PrimitiveType.Sphere; // нечего сюда вставить. Уж не обессудьте.
//    }
//
//    public Color GetColor()
//    {
//        return color.GetColor();
//    }
//
//    public Vector3 GetPosition()
//    {
//        return position.GetPosition();
//    }
//}