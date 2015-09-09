using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseLevelManager : MonoBehaviour {

	public static BaseLevelManager inst;

	public List <ElementsBody> teamsList = new List<ElementsBody>();
	public Dictionary <int, ElementsBody> teams = new Dictionary<int, ElementsBody> ();

	public GameObject BaseElementPrefab;

	//temporary test list of preset elements
	public List <BaseElement> testElements = new List<BaseElement> ();

	void Awake(){
		inst = this;
	}
	// Use this for initialization
	public IEnumerator Start () {
		//load environment

		// create elements id's dictionaries and lists
		for (int i = 0; i < testElements.Count; i++) {
			ElementsManager.AddElement(testElements[i]);
		}
		Debug.Log ("ids set");
		yield return new WaitForSeconds (0.1f);
		//initialize al the elements (compound entities)
		for (int i = 0; i < testElements.Count; i++) {
			System.Type entityType = new ElementsBody ().GetType();
			if(testElements[i].GetType() == entityType){
				testElements[i].InitializeElement();
			}
		}
		for (int i = 0; i < testElements.Count; i++) {
			if (!testElements [i].ready)
				testElements [i].InitializeElement ();
		}
		Debug.Log ("entities initialized");
		//activate prepared elements
		yield return new WaitForSeconds (0.1f);
		for (int i = 0; i < testElements.Count; i++) {
			testElements[i].ActivateElement();
		}
		Debug.Log ("elements activated");
	}

	void FormDictionaries(){
		for (int i = 0; i < teamsList.Count; i++) {
			teams.Add(teamsList[i].id, teamsList[i]);
		}
	}

	public void StartBattle(){
		FormDictionaries ();
		for (int i = 0; i < teamsList.Count; i++) {
			teamsList[i].InitializeBody();
		}
	}

	public virtual IEnumerator CreateBattleField(int elNum, float minDist, float maxDist, float fielWidth, float fieldLength){
		yield return new WaitForEndOfFrame ();
	for (int i = 0; i < elNum; i++) {

		}
	}

	public void PassElementBetweenTeams(BaseElement _be, int from, int to){
		Debug.Log ("element passes from team "+from.ToString()+" to "+to.ToString());
		//change team ownership
		_be.elProperties [PropertyType.TeamID].val = to;
		//atacker num to default
		_be.elProperties [PropertyType.AttackerNum].val = -1;
		//change sign of V
		_be.elProperties [PropertyType.V].val *= -1;

		teams [from].elements.Remove (_be);
		teams [to].elements.Add (_be);

	}

	public ElementsBody GetTeam (int t){
		for (int i = 0; i < teamsList.Count; i++) {
			if(teamsList[i].id == t) return teamsList[i]; 
		}
		return null;
	}

	public void FinishBattle(){
		if(BattleStatistics.inst.playerElements == BattleStatistics.inst.totalElements){
			//win
			int earnedPoints = (BattleStatistics.inst.totalElements * 30) - (int)(Time.time - BattleStatistics.inst.startTime);
			FinishScreenGUI.inst.InitializeFinishScreen("VICTORY!", earnedPoints.ToString());
		}
		if(BattleStatistics.inst.playerElements == 0){
			//lose
			FinishScreenGUI.inst.InitializeFinishScreen("BAD LUCK", ":(");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
