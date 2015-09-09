using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum SlotElemetType {Background, Title, Description, Value, Select, Icon}

public class BaseSlot : MonoBehaviour {
	public EditedOnjectIdentificator objectIdentificator;
	public SlotElement[] slotElements;

	public Color prevColor;

	// Use this for initialization
	void Start () {
		prevColor = GetSlotElement (SlotElemetType.Background).image.color;
	}

	public SlotElement GetSlotElement(SlotElemetType _elementType){
	for (int i = 0; i < slotElements.Length; i++) {
			if(slotElements[i].elType == _elementType){
				return slotElements[i];
			}
		}
		return null;
	}

	public void SelectSlot(){
		prevColor = GetSlotElement (SlotElemetType.Background).image.color;
		GetSlotElement (SlotElemetType.Background).image.color = ElementInfoManager.inst.selectedSlotColor;
	}
	public void DeselectSlot(){
		GetSlotElement (SlotElemetType.Background).image.color = prevColor;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class SlotElement{
	public SlotElemetType elType;
	public Image image;
	public Text text;
}

