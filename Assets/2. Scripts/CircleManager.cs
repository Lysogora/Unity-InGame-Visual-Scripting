using UnityEngine;
using System.Collections;

public class CircleManager : BaseElement {

	public float radius;
	public Transform[] elements;
	public float[] speeds;
	// Use this for initialization
	public void Start () {
		InitializeElement ();
	}
	public override void InitializeElement(){
		base.InitializeElement ();
		
		//get all functions
		BaseElementFunction[] elFuncs = this.gameObject.GetComponents < BaseElementFunction> ();
		for (int i = 0; i < elFuncs.Length; i++) {

		}		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float prevR = 0;
		if (prevR != radius) {
			prevR = radius;
			for (int i = 0; i < elements.Length; i++) {
				elements[i].position = elements[i].position.normalized * radius;
			}
		}
		for (int i = 0; i < elements.Length; i++) {
			elements[i].RotateAround(this.transform.position, Vector3.up, speeds[i]*Time.deltaTime);
			elements[i].rotation = Quaternion.LookRotation(elements[i].position - this.transform.position, Vector3.up);
		}
	}
}
