using UnityEngine;
using System.Collections;

public class GameCameraControl : MonoBehaviour {



	public static GameCameraControl inst;

	//general view vars
	float lookSpeed = 10.0f;
	float moveSpeed = 10.0f;
	
	float rotationX = 0.0f;
	float rotationY = 0.0f;

	public float camtHeight;
	public Vector3 startRot;

	//ship follow vars
	public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public bool followBehind = true;
	public float rotationDamping = 10.0f;
	public float freeRotationDamping = 10.0f;
	public float aboveHeight = 5;
	
	private float wantedHeight;
	private float wantedRotationAngle;
	private float currentRotationAngle;
	private float currentHeight;
	private Quaternion newRotation;
	
	private Quaternion currentRotation;

	void Awake(){
		inst = this;
		transform.position = new Vector3 (0, camtHeight, 0);
		transform.eulerAngles = startRot;
	}
	
	void LateUpdate ()
	{
//		if (GameManager.inst.combatRole == CombatRole.GeneralCommander) {
//			if (Input.GetMouseButton (1)) {
//				rotationX += Input.GetAxis ("Mouse X") * lookSpeed;
//				rotationY += Input.GetAxis ("Mouse Y") * lookSpeed;
//				rotationY = Mathf.Clamp (rotationY, -90, 90);
//		
//				Quaternion newRot = Quaternion.AngleAxis (rotationX, Vector3.up) * Quaternion.AngleAxis (rotationY, Vector3.left);
//				transform.localRotation = Quaternion.Slerp(transform.localRotation, newRot, freeRotationDamping);
//			}
//		
//
//			//transform.position += transform.forward * Time.deltaTime * moveSpeed * Input.GetAxis ("Vertical");
//			//transform.position += transform.right * moveSpeed * Time.deltaTime * Input.GetAxis ("Horizontal");
//			Vector3 newForward = transform.TransformPoint(Vector3.forward);
//			newForward = (new Vector3(newForward.x, camtHeight, newForward.z) - transform.position).normalized * Time.deltaTime * moveSpeed * Input.GetAxis ("Vertical");
//			transform.position = Vector3.Lerp(transform.position, transform.position + newForward , 5);
//			transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis ("Horizontal")), 5);
//		} 
	}


}