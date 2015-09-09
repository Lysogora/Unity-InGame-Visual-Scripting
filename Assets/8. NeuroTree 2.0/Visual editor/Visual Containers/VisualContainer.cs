using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VisualContainer : MonoBehaviour {
	public string name;
	public float[] size = new float[2];
	public float[] position = new float[2];
	public BaseNode node;
	public List <VisualElement> vElements = new List<VisualElement> ();

	public Text title;
	public Image typeImg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
