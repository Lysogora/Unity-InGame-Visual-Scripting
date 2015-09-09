using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectionScreen : MonoBehaviour {

	public List <LevelSelectionSlot> slots = new List<LevelSelectionSlot> ();

	public static LevelSelectionScreen inst;

	void Awake(){
		inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void OnEnable(){
	for (int i = 0; i < slots.Count; i++) {
			slots[i].numLabel.text = (i+1).ToString();
			slots[i].num = i;
		}
	}

	public void StartlLevel(int _num){
		GameManager.inst.GoToBattle (_num);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
