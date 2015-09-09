using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleStatistics : MonoBehaviour {

	public int playerElements;
	public int totalElements;
	public float startTime;

	public static BattleStatistics inst;

	void Awake(){
		inst = this;
	}

	// Use this for initialization
	public IEnumerator Start () {
		bool startMainCoroutine = false;
		while(!startMainCoroutine){
			if(BaseLevelManager.inst != null && BaseLevelManager.inst.teamsList.Count > 0 && BaseLevelManager.inst.teams.ContainsKey(1)){
				startMainCoroutine = true;
				StartCoroutine(StatisticsCoroutine());
				for (int t = 0; t < BaseLevelManager.inst.teamsList.Count; t++) {
					totalElements+=BaseLevelManager.inst.teamsList[t].elements.Count;
				}
			}
			yield return new WaitForSeconds(0.1f);
		}

	}

	public IEnumerator StatisticsCoroutine(){
		startTime = Time.time;
		while (true) {
			playerElements = BaseLevelManager.inst.teams[1].elements.Count;
			if(playerElements == totalElements){
				//win
				BaseLevelManager.inst.FinishBattle();
			}
			if(playerElements == 0){
				//lose
				BaseLevelManager.inst.FinishBattle();
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
