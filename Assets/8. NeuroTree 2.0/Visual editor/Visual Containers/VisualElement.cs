using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VisualElement : MonoBehaviour {
	public List <ConnectionUI> conUIs = new List<ConnectionUI> ();
	public Text elName;
	public Image elImg;

	// Use this for initialization
	void Start () {
	
	}

	public void AddConnection(ConnectionUI conUI){
		if (!conUIs.Contains (conUI)) {
			conUIs.Add(conUI);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
