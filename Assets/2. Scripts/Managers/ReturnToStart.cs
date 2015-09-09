using UnityEngine;
using System.Collections;

public class ReturnToStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void ExitScene(){
		if (GameManager.inst != null) {
			GameManager.inst.GoToStart();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
