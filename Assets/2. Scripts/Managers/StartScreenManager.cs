using UnityEngine;
using System.Collections;

public class StartScreenManager : MonoBehaviour {

	public GameObject startScreen;
	public GameObject levelSelectionScreen;
	public GameObject settingsScreen;
	public GameObject creditsScreen;

	public static StartScreenManager inst;


	void Awake(){
		inst = this;
	}
	// Use this for initialization
	void Start () {
	
	}

	void OnEnable(){
		ShowStartScreen ();
	}

	public void ShowLevelSelection(){
		startScreen.SetActive (false);
		creditsScreen.SetActive (false);
		settingsScreen.SetActive (false);
		levelSelectionScreen.SetActive (true);
	}

	public void ShowSettings(){
		startScreen.SetActive (false);
		creditsScreen.SetActive (false);
		settingsScreen.SetActive (true);
		levelSelectionScreen.SetActive (false);
	}

	public void ShowCredits(){
		startScreen.SetActive (false);
		creditsScreen.SetActive (true);
		settingsScreen.SetActive (false);
		levelSelectionScreen.SetActive (false);
	}

	public void ShowStartScreen(){
		startScreen.SetActive (true);
		creditsScreen.SetActive (false);
		settingsScreen.SetActive (false);
		levelSelectionScreen.SetActive (false);
	}

	public void CreateNewPlayer(){
		UserManager.inst.CreateDefaultUser ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
