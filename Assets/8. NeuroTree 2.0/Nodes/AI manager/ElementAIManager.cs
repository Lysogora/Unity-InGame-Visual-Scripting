using UnityEngine;
using System.Collections;

[System.Serializable]
public class ElementAIManager{

	public int experience;
	
	public int Level
	{
		get{ return experience / 750; }}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
