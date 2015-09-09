using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fpsCounter : MonoBehaviour {

	public Text counterText;

	// Use this for initialization
	void Start () {
		StartCoroutine (ShowFPS ());
	}

	IEnumerator ShowFPS(){
		while (true) {
			counterText.text = (1.0f / Time.deltaTime).ToString ("F2");
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
