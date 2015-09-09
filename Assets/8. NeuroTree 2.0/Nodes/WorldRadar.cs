using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldRadar : MonoBehaviour {
	public float radius;
	public float refreshRate;
	public List <int> notisedElements;

	public Blackboard blackboard;
	// Use this for initialization
	void Start () {
		StartCoroutine (RadarCoroutine ());
	}

	public IEnumerator RadarCoroutine(){
		while (true) {
			UpdateRadar();
			yield return new WaitForSeconds (refreshRate);
		}
	}

	public void UpdateRadar(){
		Collider[] rawElements = Physics.OverlapSphere (this.transform.position, radius);
//		blackboard.baseElements.Clear ();
//		blackboard.activeElements.Clear ();
//
//		for (int i = 0; i < rawElements.Length; i++) {
//			BaseElement be = rawElements[i].gameObject.GetComponent<BaseElement>();
//			if(be != null && be != blackboard.subject){
//				blackboard.baseElements.Add(be);
//				BaseActivityElement bae = (BaseActivityElement)be;
//				if(bae != null){
//					blackboard.activeElements.Add(bae);
//				}
//			}
//		}
//
//		blackboard.baseElementsPriority = new float[blackboard.baseElements.Count];
//		blackboard.activityElementsPriority = new float[blackboard.activeElements.Count];

		notisedElements.Clear ();
		for (int i = 0; i < rawElements.Length; i++) {
			BaseElement be = rawElements[i].gameObject.GetComponent<BaseElement>();
			if (be != null){
				notisedElements.Add(be.id);
			}
		}
		blackboard.UpdateTable (notisedElements);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
