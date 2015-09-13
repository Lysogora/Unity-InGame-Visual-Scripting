using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VisualContainer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public string name;
	public float[] size = new float[2];
	public float[] position = new float[2];
	public BaseNode node;
	public List <VisualElement> vOutElements = new List<VisualElement> ();
	public List <VisualElement> vInElements = new List<VisualElement> ();

	public Transform inputTrans;
	public Transform outputTrans;

	public Text title;
	public Image typeImg;


	public RectTransform rTrans;
	public bool dragging;
	public Vector3 offset;
	public Vector3 eventPosition;
	public float scaleQx;
	public float scaleQy;

	// Use this for initialization
	void Start () {
	
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData){
		dragging = true;
		scaleQx = 1900.0f / Screen.width;
		scaleQy = 1080.0f / Screen.height;
		offset = (-Input.mousePosition + VisualScriptEditor.inst.UIcam.WorldToScreenPoint (rTrans.position));
		offset = new Vector3 (offset.x * scaleQy, offset.y, offset.z);
		//offset = Vector2.zero;

	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData){
		eventPosition = Input.mousePosition;
		Vector3 newPos = new Vector3(Input.mousePosition.x*scaleQx, Input.mousePosition.y * scaleQy, Input.mousePosition.z) 
			+ offset - new Vector3(1900.0f/2, 1080.0f/2, 0);
		rTrans.localPosition = newPos;
	}

	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData){
		dragging = false;
	}
	#endregion

	
	// Update is called once per frame
	void Update () {
	
	}
}
