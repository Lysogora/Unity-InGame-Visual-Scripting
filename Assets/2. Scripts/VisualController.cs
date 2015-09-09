using UnityEngine;
using System.Collections;

public enum ControlLevel{Base, Agent, AttackSelection, MoveSelection, EnergySelection};

public class VisualController : MonoBehaviour {


	public BaseElement processingAgent;
	public string coliderName;
	public ControlLevel controlLevel;

	public Transform HUDsection;

	public GameObject agentVC;

	public GameObject agentVCprefab;
	public GameObject VCprefab;

	public static VisualController inst;

	public Renderer pointedRenderer;
	public Shader normalShader;
	public Shader outlineShader;

	public bool agentSelected;

	float timestamp;

	public LineRenderer selDir;

	void Awake(){
		if (inst == null)
			inst = this;
	}

	// Use this for initialization
	void Start () {
		//StartCoroutine(ProcessPointerCoroutine());
		normalShader = Shader.Find ("Toony Colors/Toony Colors");
		outlineShader = Shader.Find ("Outlined/Silhouetted Diffuse");
	}

	void CreateAgentSign(){
	
	}

	public IEnumerator ProcessPointerCoroutine(){
		while (true) {
			ProcessDownPointer();
			yield return new WaitForSeconds(0.3f);
		}
	}

	public BaseElement GetElement(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000)) {
			BaseElement be = hit.collider.gameObject.GetComponent<BaseElement> ();
			if (be != null) {
				Debug.Log("GOT SOME ELEMENT");
				return be;				
			}
			else{
				return null;
			}
		}
		else{
			return null;
		}
	}

	public void ProcessDownPointer(){
		BaseElement _agent = GetElement ();
		if (_agent != null) {
			processingAgent = _agent;
			agentSelected = true;
			ShowSelectionDirection(true);
		}
	}
	public void ProcessUpPointer(){
		BaseElement _agent = GetElement ();
		if (_agent != null && agentSelected) {
			Debug.Log("UP ON SOME ELEMENT");
			if (_agent == processingAgent) {
				Debug.Log("UP ON THE SAME ELEMENT");
				BaseActivityElement bae = _agent as BaseActivityElement;
				if(bae == null){
					ElementInfoManager.inst.ProcessElement(_agent);
				}
				else{
					ElementInfoManager.inst.ProcessElement(bae);
				}
			} 
			IElementActivity<BaseElement, FunctionType> iActivity = processingAgent as IElementActivity<BaseElement, FunctionType>;
			if(iActivity != null){
				iActivity.AddTarget(_agent);
				iActivity.Do();
			}


		} else {
//			processingAgent.SetAgentLevelAgentTarget (null, TacticsType.Attack);
		}
		ShowSelectionDirection (false);
		agentSelected = false;

	}

	public void AllTimePointerProcessing(){
//		bool isEnemy = false;
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		RaycastHit hit;
//		if (Physics.Raycast (ray, out hit, 1000, 1)) {
//			Renderer currentRenderer = null;
//			IHiLAB_Subject subj = hit.collider.gameObject.GetComponent<IHiLAB_Subject>();
//			BaseElement a = hit.collider.gameObject.GetComponent<BaseElement>();
//			BaseDamageReceiver bdr = hit.collider.gameObject.GetComponent<BaseDamageReceiver>();
//			if(subj != null){
//				currentRenderer = subj.agent.cRenderer;
//				if(subj.agent.player != BattleManager.inst.player){
//					isEnemy = true;
//				}
//			}
//			else if(a!= null){
//				currentRenderer = a.cRenderer;
//				if(a.player != BattleManager.inst.player){
//					isEnemy = true;
//				}
//			}
//			else if(bdr!= null){
//				currentRenderer = bdr.subject.agent.cRenderer;
//				if(bdr.subject.agent.player != BattleManager.inst.player){
//					isEnemy = true;
//				}
//			}
//			if (currentRenderer != null ){
//				if(currentRenderer != pointedRenderer){
//					if(BattleGUIControler.inst.rMode == RepresentationMode.Real){
//						if(pointedRenderer != null){
//							pointedRenderer.material.shader = normalShader;
//							pointedRenderer = currentRenderer;
//							pointedRenderer.material.shader = outlineShader;
//							if(isEnemy){
//								pointedRenderer.material.SetColor("_OutlineColor", Color.red);
//							}
//							else{
//								pointedRenderer.material.SetColor("_OutlineColor", Color.white);
//							}
//						}
//						else {
//							pointedRenderer = currentRenderer;
//							pointedRenderer.material.shader = outlineShader;
//							if(isEnemy){
//								pointedRenderer.material.SetColor("_OutlineColor", Color.red);
//							}
//							else{
//								pointedRenderer.material.SetColor("_OutlineColor", Color.white);
//							}
//						}
//					}
//				}
//			}
//			else{
//				if(pointedRenderer != null){
//					pointedRenderer.material.shader = normalShader;
//					pointedRenderer = null;
//				}
//			}
//		}
//		else{
//			if(pointedRenderer != null){
//				pointedRenderer.material.shader = normalShader;
//				pointedRenderer = null;
//			}
//		}
	}

	void ShowSelectionDirection(bool b){
		selDir.gameObject.SetActive (b);

	}

	public void ShowShipControlButtons(BaseElement _agent){
//		Destroy(agentVC);
//		processingAgent = null;
//
//		processingAgent = _agent;
//		GameObject tagentVC = Instantiate(agentVCprefab, Vector3.zero, Quaternion.identity) as GameObject;	
//		agentVC = tagentVC;
//		agentVC.transform.SetParent(HUDsection);
//		agentVC.transform.localPosition = Vector3.zero;
//		agentVC.GetComponent<AgentVC>().InitializeVC(_agent, _agent.cTransform, _agent.cRenderer, this);
//		agentVC.GetComponent<AgentVC>().ShowFulVisualControl();
	}
	
	// Update is called once per frame
	void Update () {
		//if (processingAgent != null) {
		if (Input.GetMouseButtonDown (0)) {
			ProcessDownPointer ();
		timestamp = Time.time;
		}
		if (Input.GetMouseButtonUp (0)) {
			ProcessUpPointer ();
		}
			
		if (Input.GetMouseButton (0)) {
			if(agentSelected){
				selDir.SetPosition(0, processingAgent.transform.position);
				Vector3 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				endPoint.y = -1;
				selDir.SetPosition(1, endPoint);
			}
		}
		//}
		//AllTimePointerProcessing ();
	}
}
