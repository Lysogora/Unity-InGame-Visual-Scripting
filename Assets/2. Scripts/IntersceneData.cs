using UnityEngine;
using System.Collections;

public class IntersceneData : MonoBehaviour {


	public static IntersceneData inst;
	// Use this for initialization
	void Awake(){
		if (inst == null) {
			inst = this;
			DontDestroyOnLoad (inst.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
