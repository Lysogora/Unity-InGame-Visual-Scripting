using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlyInfo : MonoBehaviour {
	public RectTransform rTrans;
	public Text textLabel;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rTrans.position += Vector3.up * 20 * Time.deltaTime;
	}
}
