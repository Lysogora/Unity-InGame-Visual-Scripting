using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBlock {

	public List<BaseNode> coreNodes = new List<BaseNode> ();
	public List<BaseNode> dataNodes = new List<BaseNode> ();

	public List<NodeVisualData> coreVisuals = new List<NodeVisualData> ();
	public List<NodeVisualData> dataNodesVisuals = new List<NodeVisualData> ();

	//visual representation data

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class NodeVisualData{
	public float[] size = new float[2];
	public float[] position = new float[2];

	public List<NodeVisualData> lowerVisualData = new List<NodeVisualData> ();
}
