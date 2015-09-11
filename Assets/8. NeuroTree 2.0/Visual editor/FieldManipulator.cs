using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class FieldManipulator : MonoBehaviour, IScrollHandler, IDragHandler {
	#region IScrollHandler implementation

	public void OnScroll (PointerEventData eventData)	{
		VisualScriptEditor.inst.ChangeFieldScale (eventData.scrollDelta.y);
	}
	#endregion

	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)	{
		VisualScriptEditor.inst.MoveField (eventData.delta);
	}
	
	#endregion



}
