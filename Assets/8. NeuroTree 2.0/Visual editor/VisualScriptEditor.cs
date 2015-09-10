using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VisualScriptEditor : MonoBehaviour {

	public Transform editorField;

	public AIBlock aiBlock;

	public List <BaseNode> processedNodes;


	//prefabs
	public GameObject containerPrefab;
	public GameObject varElementPrefab;
	public GameObject connectionPrefab;


	// Use this for initialization
	void Start () {
	
	}
	float xOffset = 0.0f;
	float yOffset = 0.0f;
	public void ShowAI(AIBlock _aiBlock){
		aiBlock = _aiBlock;

		xOffset = 0.0f;
		yOffset = 0.0f;
		processedNodes = new List<BaseNode> ();

		for (int i = 0; i < aiBlock.coreNodes.Count; i++) {
			yOffset = 0.0f;

			processedNodes.Add(aiBlock.coreNodes[i]);
			GameObject visualContainer = Instantiate(containerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			visualContainer.transform.SetParent(editorField);
			//connect to node

			//

			if(aiBlock.coreVisuals.Count > i){

			}
			else{
				visualContainer.transform.localPosition = new Vector3 (xOffset + 50.0f, yOffset + 50.0f, 0);
				xOffset += 100.0f;
				yOffset -= 100.0f;
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
	}

	void ShowNextTier(List <BaseNode> _nodes, List <NodeVisualData> _visuals){
		for (int i = 0; i < _nodes.Count; i++) {
			GameObject visualContainer = Instantiate (containerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			visualContainer.transform.SetParent (editorField);
			//connect to node
		
			//
			if (_visuals.Count > i) {
		
			} else {
				visualContainer.transform.localPosition = new Vector3 (xOffset + 50.0f, yOffset + 50.0f, 0);
				xOffset += 100.0f;
				yOffset -= 100.0f;
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
