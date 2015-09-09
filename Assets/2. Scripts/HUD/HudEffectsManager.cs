using UnityEngine;
using System.Collections;

public class HudEffectsManager : MonoBehaviour {

	public static HudEffectsManager inst;
	public GameObject flyUpInfoPrefab;
	public GameObject wavePrefab;

	void Awake(){
		inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void ShowFlyUpInfo(string t, Vector3 pos){
		//if (BattleManager.inst.mainCam.transform.InverseTransformPoint (pos).z > 0) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (pos);
			GameObject go = Instantiate (flyUpInfoPrefab, this.transform.position, Quaternion.identity) as GameObject;
			go.GetComponent<FlyInfo> ().textLabel.text = t;
			go.transform.SetParent (this.transform);
			go.GetComponent<FlyInfo> ().rTrans.position = screenPos;
		//}
	}

	public void ShowAttackCircles(Vector3 _position, Color _color){
		GameObject go = Instantiate (wavePrefab, _position, Quaternion.identity) as GameObject;
		go.GetComponent<ElementWave> ().InitializeWave (_color);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
