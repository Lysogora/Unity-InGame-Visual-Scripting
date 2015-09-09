using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BaseTouchElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	// Use this for initialization
	void Start () {
	
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
	}

	#endregion
	
	// Update is called once per frame
	void Update () {
	
	}
}
