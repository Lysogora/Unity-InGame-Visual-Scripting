using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum TaskResult {Success, Fail, Wait};

public class TestDlelegateManager : MonoBehaviour {

	//public delegate bool Func<out bool>();
	public List <BaseTask> tasks = new List<BaseTask> ();

	public List <Func<TaskResult>> myFuncs  = new List<Func<TaskResult>> ();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < tasks.Count; i++) {
				myFuncs.Add(tasks[i].Run);
			}
		for (int i = 0; i < myFuncs.Count; i++) {
				TaskResult result = myFuncs[i].Invoke();
			Debug.Log(result.ToString());
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
