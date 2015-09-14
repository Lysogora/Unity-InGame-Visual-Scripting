using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[System.Serializable]
public class VisualElement : MonoBehaviour, IPointerDownHandler {
	public VisualContainer vContainer;
	public NodeConnection nodeConnection;
	public List <ConnectionUI> conUIs = new List<ConnectionUI> ();
	public Text elName;
	public Image elImg;

	// Use this for initialization
	void Start () {
	
	}

	public void InitializeVisualElement(VisualContainer _vContainer, NodeConnection _nodeConnection){
		vContainer = _vContainer;
		nodeConnection = _nodeConnection;

		//Get Names for our variables
		if (nodeConnection.dataDirection == DataDirection.OutcomeData) {
			//adjust name position

			if(vContainer.node.outConNames.ContainsKey(nodeConnection.varType)){
				if(vContainer.node.outConNames[nodeConnection.varType].Count > nodeConnection.num){
					elName.text = vContainer.node.outConNames[nodeConnection.varType][nodeConnection.num];
				}
			}

		}

		if (nodeConnection.dataDirection == DataDirection.IncomeData) {
			//adjust name position
			elName.alignment = TextAnchor.MiddleLeft;
			elName.rectTransform.anchoredPosition = new Vector2(elName.rectTransform.anchoredPosition.x * -1, elName.rectTransform.anchoredPosition.y);
			
			if(vContainer.node.inConNames.ContainsKey(nodeConnection.varType)){
				if(vContainer.node.inConNames[nodeConnection.varType].Count > nodeConnection.num){
					elName.text = vContainer.node.inConNames[nodeConnection.varType][nodeConnection.num];
				}
			}
			
		}
	}

	public void AddConnection(ConnectionUI conUI){
		if (!conUIs.Contains (conUI)) {
			conUIs.Add(conUI);
		}
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)	{
		ProcesClick ();
	}

	#endregion

	public void ProcesClick(){
		Debug.Log ("Pointer click");
		if (Input.GetMouseButtonDown (1)) {
			Debug.Log ("delete command");
			VisualScriptEditor.inst.BreakConnection(nodeConnection);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
