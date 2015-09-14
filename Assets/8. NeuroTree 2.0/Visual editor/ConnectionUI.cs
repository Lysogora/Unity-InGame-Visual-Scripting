using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectionUI : MonoBehaviour {

	public VisualElement visElement;
	public VisualElement otherVisElement;


	public NodeConnection nodeConnection;
	public ConnectionUI otherConnectionUI;
	public int linePointsNum;
	public List <Vector3> points = new List <Vector3> ();

	public LineRenderer lineRenderer;
	public Transform beginTrans;
	public Transform endTrans;

	// Use this for initialization
	void Start () {
	
	}

	public void InitializeConnectionUI(VisualElement _visElement, VisualElement _otherVisElement){
		visElement = _visElement;
		otherVisElement = _otherVisElement;
		nodeConnection = visElement.nodeConnection;

		beginTrans = visElement.transform;
		endTrans = otherVisElement.transform;

	}

	public void SetLinePoints(Transform point1, Transform point2){
		beginTrans = point1;
		endTrans = point2;
	}
	
	// Update is called once per frame
	public Vector3 offset;
	public Vector2 p1;
	public Vector2 p2;
	void Update () {
		if (beginTrans != null && endTrans != null) {
			lineRenderer.SetPosition(0, beginTrans.position + offset);
			lineRenderer.SetPosition(1, endTrans.position + offset);
		}
	
	}
}
