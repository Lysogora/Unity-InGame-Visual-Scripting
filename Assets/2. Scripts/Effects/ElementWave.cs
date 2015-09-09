using UnityEngine;
using System.Collections;

public class ElementWave : MonoBehaviour {

	public ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
	
	}

	public void InitializeWave(Color c){
		pSystem.startColor = c;
		pSystem.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
