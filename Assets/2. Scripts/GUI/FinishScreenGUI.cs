using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishScreenGUI : MonoBehaviour {

	public Text upperText;
	public Text pointsText;

	public GameObject screen;
	public static FinishScreenGUI inst;

	// Use this for initialization
	void Awake (){
		inst = this;
		screen.SetActive (false);
	}

	void Start () {
	
	}

	public void InitializeFinishScreen(string _upperText, string _points){
		screen.SetActive (true);
		upperText.text = _upperText;
		pointsText.text = _points;
	}

	public void Repeat(){

	}

	public void Share(){

	}

	public void Next(){

	}

	public void NoAds(){

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
