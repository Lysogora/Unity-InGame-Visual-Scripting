using UnityEngine;
using System.Collections;

public class Task1 : BaseTask {

	// Use this for initialization
	void Start () {
	
	}

	public override TaskResult Run ()
	{
		Debug.Log ("This is task 1");
		return TaskResult.Success;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
