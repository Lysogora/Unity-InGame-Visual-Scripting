using UnityEngine;
using System.Collections;

public class Task2 : BaseTask {
	
	// Use this for initialization
	void Start () {
		
	}
	
	public override TaskResult Run ()
	{
		Debug.Log ("This is task 2");
		return TaskResult.Fail;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}