using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectionUI : MonoBehaviour {

	public int linePointsNum;
	public List <Vector3> points = new List <Vector3> ();
	public UILineRenderer lineRend;

	public Transform beginTrans;
	public Transform endTrans;

	// Use this for initialization
	void Start () {
	
	}

//	public void SetLinePoints(Vector3 point1, Vector3 point2){
//		lineRend.SetPosition (0, point1);
//		lineRend.SetPosition (1, point2);
//	}
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
//			lineRend.Points[0] = beginTrans.position + offset;
//			lineRend.Points[1] = endTrans.position + offset;
			p1 = beginTrans.GetComponent<RectTransform>().rect.position;;
			p2 = endTrans.GetComponent<RectTransform>().rect.position;
			lineRend.Points[0] = p1;
			lineRend.Points[1] = p2;
		}
	
	}
}
