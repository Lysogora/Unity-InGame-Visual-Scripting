using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class VisualScriptEditor : MonoBehaviour {

	public static VisualScriptEditor inst;

	public Transform editorField;
	public RectTransform editorRtrans;

	public AIBlock aiBlock;

	//processing Data
	public List <BaseNode> processedNodes;
	public List <VisualContainer> nodeVisuals;


	//prefabs
	public GameObject containerPrefab;
	public GameObject varElementPrefab;
	public GameObject connectionPrefab;

	public CanvasScaler canvasScaler;


	void Awake(){
		if (inst == null)
			inst = this;

	}

	// Use this for initialization
	void Start () {
	
	}
	public float xStep = 300.0f;
	public float yStep = 150.0f;
	float xOffset = 0.0f;
	float yOffset = 0.0f;


	public void ShowAI(AIBlock _aiBlock){
		aiBlock = _aiBlock;

		xOffset = -600.0f;
		yOffset = 350.0f;
		processedNodes = new List<BaseNode> ();
		nodeVisuals = new List<VisualContainer> ();
		for (int i = 0; i < aiBlock.coreNodes.Count; i++) {
			yOffset = 350.0f;

			processedNodes.Add(aiBlock.coreNodes[i]);
			GameObject visualContainer = Instantiate(containerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			visualContainer.transform.SetParent(editorField);
			visualContainer.transform.localScale = Vector3.one;
			//connect to node
			VisualContainer container = visualContainer.GetComponent<VisualContainer>();
			nodeVisuals.Add (container);
			container.node = aiBlock.coreNodes[i];
			container.title.text = container.node.GetType().ToString();
			//

			if(aiBlock.coreVisuals.Count > i){

			}
			else{
				visualContainer.transform.localPosition = new Vector3 (xOffset + xStep/2, yOffset + yStep/2, 0);
				xOffset += xStep;
				yOffset -= yStep;
			}
			if (aiBlock.coreNodes[i].lowerNodes != null){
				if (aiBlock.coreVisuals.Count > i && aiBlock.coreVisuals[i].lowerVisualData != null){
					ShowNextTier(aiBlock.coreNodes[i].lowerNodes, aiBlock.coreVisuals[i].lowerVisualData);
				}
				else{
					ShowNextTier(aiBlock.coreNodes[i].lowerNodes, null);
				}
			}
		}

		// spawning blocks of additional data blocks that are not part of hierarchy
		for (int i = 0; i < aiBlock.dataNodes.Count; i++) {
			yOffset = -350.0f;
			
			processedNodes.Add(aiBlock.dataNodes[i]);
			GameObject visualContainer = Instantiate(containerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			visualContainer.transform.SetParent(editorField);
			visualContainer.transform.localScale = Vector3.one;
			//connect to node
			VisualContainer container = visualContainer.GetComponent<VisualContainer>();
			nodeVisuals.Add (container);
			container.node = aiBlock.dataNodes[i];
			container.title.text = container.node.GetType().ToString();
			//			
			if(aiBlock.dataNodesVisuals.Count > i){
				
			}
			else{
				visualContainer.transform.localPosition = new Vector3 (xOffset + xStep/2, yOffset + yStep/2, 0);
				xOffset += xStep;
				yOffset -= yStep;
			}
			if (aiBlock.dataNodes[i].lowerNodes != null){
				if (aiBlock.dataNodesVisuals.Count > i && aiBlock.dataNodesVisuals[i].lowerVisualData != null){
					ShowNextTier(aiBlock.dataNodes[i].lowerNodes, aiBlock.dataNodesVisuals[i].lowerVisualData);
				}
				else{
					ShowNextTier(aiBlock.dataNodes[i].lowerNodes, null);
				}
			}
		}
	}

	void ShowNextTier(List <BaseNode> _nodes, List <NodeVisualData> _visuals){
		for (int i = 0; i < _nodes.Count; i++) {
			GameObject visualContainer = Instantiate (containerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			visualContainer.transform.SetParent (editorField);
			visualContainer.transform.localScale = Vector3.one;
			//connect to node
			VisualContainer container = visualContainer.GetComponent<VisualContainer>();
			nodeVisuals.Add (container);
			container.node = aiBlock.dataNodes[i];
			container.title.text = container.node.GetType().ToString();
			//
			if (_visuals.Count > i) {
		
			} else {
				visualContainer.transform.localPosition = new Vector3 (xOffset + xStep/2, yOffset + yStep/2, 0);
				xOffset += xStep;
				yOffset -= yStep;
			}
			if (_nodes[i].lowerNodes != null){
				if (_visuals.Count > i && _visuals[i].lowerVisualData != null){
					ShowNextTier(_nodes[i].lowerNodes, _visuals[i].lowerVisualData);
				}
				else{
					ShowNextTier(_nodes[i].lowerNodes, null);
				}
			}
		}
	}

	void ShowNodeElements(VisualContainer _container){
		for (int i = 0; i < _container.node.inConnections.Count; i++) {
			GameObject element = Instantiate(varElementPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			element.transform.SetParent(_container.inputTrans);
		}

		for (int i = 0; i < _container.node.outConnections.Count; i++) {
			GameObject element = Instantiate(varElementPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			element.transform.SetParent(_container.outputTrans);
		}
	}

	void ShowInputNodeConnections(VisualContainer _container){
	
	}

	// scaling of the field with containers
	public float fieldScale = 1.0f;
	public float scaleSensitivity = 0.1f;
	public void ChangeFieldScale(float f){
		fieldScale += f * Time.deltaTime * scaleSensitivity;
		editorRtrans.localScale = Vector3.one * fieldScale;
	} 


	public void MoveField(Vector2 delta){
		editorField.transform.localPosition += new Vector3 (delta.x, delta.y, 0);
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
