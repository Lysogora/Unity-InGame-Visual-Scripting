using UnityEngine;
using System.Collections;
using NeuroTree;

public class UserManager : MonoBehaviour {

	public string userName;
	public int activePlayerID;
	public int playerID;
	public LevelConfiguration userPlayer;

	public bool browserVersion;
	public bool rssetDefaultPlayer;

	public static UserManager  inst;


	void Awake(){
		if (inst == null)
			inst = this;
	}

	// Use this for initialization
	public IEnumerator Start () {
		yield return new WaitForSeconds (0.5f);


		//LoadPreviousPlayer ();
	}

	public void LoadPreviousPlayer(){
		activePlayerID = PlayerPrefs.GetInt ("ActivePlayerID", 0);
		if (activePlayerID != 0) {
			if(!browserVersion){
				userPlayer = SaveAndLoad.inst.LoadLevel(activePlayerID.ToString());
			}
			else if (browserVersion){
				userPlayer = SaveAndLoad.inst.LoadPlayerAtPlayerPrefs(activePlayerID.ToString());
			}

			if(userPlayer == null){
				PlayerPrefs.SetInt ("ActivePlayerID", 0);
				Debug.Log("Player Load failed");
			}
			else{
				Debug.Log ("Active player has been loaded");
			}
		}
		else if (activePlayerID == 0){
			CreateDefaultUser();
			Debug.Log ("New player has been created");
		}
	}

	//create new player with 5 ships
	public void CreateDefaultUser(){
		//create default reflector
		LevelConfiguration newPlayer = new LevelConfiguration ();
		//save player reflector
		if (newPlayer != null) {
			userPlayer = newPlayer;
			if (!browserVersion)
				SaveAndLoad.inst.SaveLevel2 (userPlayer);
			else{
				SaveAndLoad.inst.SavePlayerAtPlayerPrefs (userPlayer);
			}

			playerID = 0;
			PlayerPrefs.SetInt ("ActivePlayerID", playerID);
			activePlayerID = PlayerPrefs.GetInt("ActivePlayerID", 0);
		}
	}

	LevelConfiguration CreateTestPlayer(){
		LevelConfiguration np = new LevelConfiguration ();

		return np;
	} 


	public void SaveActivePlayer(){
		SaveAndLoad.inst.SaveLevel2 (userPlayer);
		Debug.Log ("Active player saved");
	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if(rssetDefaultPlayer){
			rssetDefaultPlayer = false;
			CreateDefaultUser();
		}
#endif
	
	}
}
