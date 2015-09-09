using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseTutorial : MonoBehaviour {

	public GameObject tutGO;
	public Text tutText;
	public bool passTutorial;


	// Use this for initialization
	void Start () {
		StartCoroutine (TutorialCoroutine ());
	}

	public IEnumerator TutorialCoroutine(){
		while (!passTutorial) {
			if(LevelManager.inst != null){
				if(LevelManager.inst.levelNum == 0){
					tutGO.SetActive(true);
					tutText.text = "Press green circle and drag to the white one. Turn all the  circles into green ones.";
				}
				else{
					tutGO.SetActive(false);
				}
				passTutorial = true;
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
