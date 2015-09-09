using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NeuroTree;

public class BioNetAI : MonoBehaviour, INTNodeNet <NodeNT> {
	#region INTNodeNet implementation
	public List<NodeNT> nodes = new List<NodeNT> ();
	public List<NodeNT> Nodes {
		get {return nodes;}
		set {nodes = value;}
	}
	#endregion

	public ElementsBody controlledBody;

	public void Awake (){
		//InitializeAI ();
	}

	public void InitializeAI(){
		NodeNT node1 = new NodeNT ();
		Nodes.Add (node1);
		node1.weight = 1.0f;

		DistanceOperation distanceOperation = new DistanceOperation ();
		node1.Operations.Add (distanceOperation);
		distanceOperation.Weight = 0.7f;
		distanceOperation.ConstraintType = OpConstraintType.FloatC;
		distanceOperation.ModType = OpModType.closeToVal;
		distanceOperation.FloatValue = 0.0f;

		//avoid targeting element which targets at your element (rule specific)
		CycleEvasionOperation cycleEvasion = new CycleEvasionOperation ();
		node1.Operations.Add (cycleEvasion);
		cycleEvasion.Weight = 1.0f;
		cycleEvasion.Initialize ();

		//prefer elements with V bellow 90%
		PropertyShareOperation notRecovered = new PropertyShareOperation ();
		node1.Operations.Add (notRecovered);
		notRecovered.ModType = OpModType.LessVal;
		notRecovered.EnumTypeVal = (int)PropertyType.V;
		notRecovered.FloatValue = 0.9f;
		notRecovered.Weight = 1.0f;

		//prefer non-own elements
		PropertyValueOperation nonOwnSeek = new PropertyValueOperation ();
		node1.Operations.Add (nonOwnSeek);
		nonOwnSeek.ModType = OpModType.NotValue;
		nonOwnSeek.EnumTypeVal = (int)PropertyType.TeamID;
		nonOwnSeek.IntValue = controlledBody.id;
		nonOwnSeek.Weight = 1.0f;



		OpModNT floatMod = new OpModNT ();
		distanceOperation.OpMods.Add (floatMod);
		floatMod.ConstraintType = OpConstraintType.FloatC;
		floatMod.ModType = OpModType.closeToVal;
		floatMod.FloatValue = 0.0f;
	}

	// Use this for initialization
	public IEnumerator Start () {
		yield return new WaitForSeconds (2.0f);
		StartCoroutine (AICoroutine());
	}

	public IEnumerator AICoroutine(){
		while (controlledBody != null) {
			for (int i = 0; i < controlledBody.elements.Count; i++) {
				//detect near lelements
				List <INTData<BaseElement>> objectsList = new List<INTData<BaseElement>>();
				Collider[] nearElements = Physics.OverlapSphere(controlledBody.elements[i].transform.position, 5.0f);
				//Debug.Log("Around "+nearElements.Length.ToString());
				for (int n = 0; n < nearElements.Length; n++) {
					if(nearElements[n].gameObject.GetComponent<BaseElement>() != controlledBody.elements[i]){
						DataNT nData = new DataNT(nearElements[n].gameObject.GetComponent<BaseElement>() , 0.0f);
						objectsList.Add(nData);
					}
				}
				//take current processing element
				List <INTData<BaseElement>> subjectsList = new List<INTData<BaseElement>>();
				subjectsList.Add(new DataNT(controlledBody.elements[i], 0.0f));
				//pass data through all operations
				for (int p = 0; p < Nodes.Count; p++) {
					Nodes[p].ProcessData(subjectsList, objectsList);
				}
				//define target by its weight
				float maxWeight = -1;
				int selectedObj = -1;
				for (int w = 0; w < objectsList.Count; w++) {
					if(objectsList[w].Weight > maxWeight){
						maxWeight = objectsList[w].Weight;
						selectedObj = w;
					}
				}
				//Debug.Log("seleced "+selectedObj+" weight "+maxWeight);
				if(selectedObj >=0){
					BaseActivityElement actEl = controlledBody.elements[i] as BaseActivityElement;
					if(actEl != null) actEl.AddTarget(objectsList[selectedObj].ObjectNT);
				}
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
