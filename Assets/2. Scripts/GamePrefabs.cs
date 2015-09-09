using UnityEngine;
using System.Collections;

public class GamePrefabs : MonoBehaviour {

	public GameObject plasmaProjectile;
	public GameObject laserProjectile;

	public GameObject energyShieldImpactPrefab;

	public GameObject minorExploisonPrefab;

	public static GamePrefabs inst;

	//HUD and GUI elements
	public GameObject shipSignPrefab;
	public GameObject shipBattleIconPrefab;
	public GameObject circleProcess_HUD;
	public GameObject textLabel_HUD;
	public GameObject rectangle_HUD;

	void Awake(){
		if (inst == null)
			inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
