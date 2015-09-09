using UnityEngine;
using System.Collections;

public class ShaderData : MonoBehaviour {

	public Shader outlineShader;
	public Shader toonShader;

	public static ShaderData inst;

	void Awake(){
		if (inst == null)
			inst = this;
	}

	// Use this for initialization
	void Start () {
		toonShader = Shader.Find ("Toony Colors/Toony Colors");
		outlineShader = Shader.Find ("Outlined/Silhouetted Diffuse");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
