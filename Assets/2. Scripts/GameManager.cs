using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public enum GameStage {StartScreen, Level};


public class GameManager : MonoBehaviour {
	public GameStage gameStage;
	public int gameVersion;

	public static GameManager inst;

	public Dictionary<GameStage, string> stageScenesDict = new Dictionary<GameStage, string>();
	void Awake (){
		if (inst == null)
			inst = this;
	}
	//COMBAT ROLE METHODS



	// Use this for initialization
	public IEnumerator Start () {
		stageScenesDict.Add(GameStage.StartScreen, "Start Screen");
		stageScenesDict.Add(GameStage.Level, "Level");
		yield return new WaitForSeconds (0.5f);
//		UserManager.inst.LoadPreviousPlayer ();
//		yield return new WaitForSeconds (0.5f);
//		if (inst != null && inst == this) {
//			StartCoroutine(LaunchGameStage(this.gameStage));
//		}
	}

	public IEnumerator LaunchGameStage(GameStage gst){
		yield return new WaitForSeconds (0.5f);
		switch (gst) {
		case GameStage.StartScreen:
			StartCoroutine(PrepareStartScreenScene());
			break;	
		case GameStage.Level:
			StartCoroutine(PrepareLevelScene());
			break;	
		default:
			break;
		}
	}

	public IEnumerator PrepareStartScreenScene(){
		yield return new WaitForEndOfFrame ();

	}

	public IEnumerator PrepareLevelScene(){
		yield return new WaitForEndOfFrame ();
		LevelManager.inst.LoadSelectedLevel (LevelManager.inst.levelNum);
		yield return new WaitForEndOfFrame ();
		BaseLevelManager.inst.StartBattle ();
	}

	public void LoadScene(GameStage gst){
		gameStage = gst;
		Application.LoadLevel (stageScenesDict[gst]);
	}


	public void GoToStart(){
		LoadScene (GameStage.StartScreen);
	}
	public void GoToBattle(int n){
		LoadScene (GameStage.Level);
		LevelManager.inst.levelNum = n;
		StartCoroutine(LaunchGameStage (GameStage.Level));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
