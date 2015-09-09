using UnityEngine;
using System.Collections;

public class BaseTask : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public virtual TaskResult Run(){
		return TaskResult.Success;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
