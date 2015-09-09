using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelSelectionSlot : MonoBehaviour, IPointerClickHandler {
	public int num;
	public Image bgr;
	public Text numLabel;
	public CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
	
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)	{
		LevelSelectionScreen.inst.StartlLevel (num);
	}

	#endregion
	
	// Update is called once per frame
	void Update () {
	
	}
}
