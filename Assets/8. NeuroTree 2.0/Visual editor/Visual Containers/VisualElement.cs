using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[System.Serializable]
public class VisualElement : MonoBehaviour, IPointerDownHandler {
	public VisualContainer vContainer;
	public VarPass varPass;
	public List <VarPassUI> conUIs = new List<VarPassUI> ();
	public Text elName;
	public Image elImg;

	// Use this for initialization
	void Start () {
	
	}

	public void InitializeVisualElement(VisualContainer _vContainer, VarPass _nodeConnection){
		vContainer = _vContainer;
		varPass = _nodeConnection;

		//Get Names for our variables
		if (varPass.dataDirection == DataDirection.OutcomeData) {
			//adjust name position

			if(vContainer.node.outConNames.ContainsKey(varPass.varType)){
				if(vContainer.node.outConNames[varPass.varType].Count > varPass.num){
					elName.text = vContainer.node.outConNames[varPass.varType][varPass.num];
				}
			}

		}

		if (varPass.dataDirection == DataDirection.IncomeData) {
			//adjust name position
			elName.alignment = TextAnchor.MiddleLeft;
			elName.rectTransform.anchoredPosition = new Vector2(elName.rectTransform.anchoredPosition.x * -1, elName.rectTransform.anchoredPosition.y);
			
			if(vContainer.node.inConNames.ContainsKey(varPass.varType)){
				if(vContainer.node.inConNames[varPass.varType].Count > varPass.num){
					elName.text = vContainer.node.inConNames[varPass.varType][varPass.num];
				}
			}
			
		}
	}

	public void AddConnection(VarPassUI conUI){
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
			VisualScriptEditor.inst.BreakConnection(varPass);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
