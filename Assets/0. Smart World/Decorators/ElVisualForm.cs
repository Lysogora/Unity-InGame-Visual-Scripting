using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ElVisualForm : MonoBehaviour {

	public BaseElement bEl;
	public BaseActivityElement bActEl;

	public SpriteRenderer outerRim;
	public SpriteRenderer innerBody;
	public SpriteRenderer mainSign;

	public bool showVstock = false; 
	// Use this for initialization
	void Start () {
		StartCoroutine (DelayBeforeElemetReady ());
	}

	public IEnumerator DelayBeforeElemetReady(){
		bool ready = false;
		while (!ready) {
			if(bActEl.elProperties.ContainsKey(PropertyType.V))
				ready = true;
			if(bActEl.elProperties.ContainsKey(PropertyType.VStock))
				showVstock = true;
			yield return new WaitForSeconds(0.1f);
		}
		StartCoroutine (VisualVHealthUpdate ());
	}

	//change inner body sprite transparency, depending on the V health of an element
	public virtual IEnumerator VisualVHealthUpdate(){
		Color fillColor = innerBody.color;
		int curVval = 0;
		while(true){
			if(bActEl.elProperties[PropertyType.V].val != curVval){
				curVval = bActEl.elProperties[PropertyType.V].val;
				fillColor.a = curVval / bActEl.elProperties[PropertyType.V].maxVal + 0.2f;
				innerBody.color = fillColor;
			}
			yield return new WaitForSeconds(0.1f);
		}
	} 
	
	// Update is called once per frame
	void Update () {
	
	}
}
