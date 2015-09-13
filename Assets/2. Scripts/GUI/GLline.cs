using UnityEngine;
using System.Collections;

public class GLline : MonoBehaviour {

	public Material lineMaterial;
	void CreateLineMaterial() {
		if( !lineMaterial ) {
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	void OnPostRender() {
		//GL.PushMatrix();
		CreateLineMaterial();
		// set the current material
		lineMaterial.SetPass( 0 );
		GL.Begin( GL.LINES );
		GL.Color( new Color(1.0f,1.0f,1.0f,0.5f) );
		GL.Vertex3( 0.0f, 0.0f, 0.0f );
		GL.Vertex3( 1.0f, 0.0f, 0.0f );
		GL.Vertex3( 0.0f, 1.0f, 0.0f );
		GL.Vertex3( 1.0f, 1.0f, 0.0f );
		GL.Color( new Color(0.0f,0.0f,0.0f,0.5f) );
		GL.Vertex3( 0.0f, 0.0f, 0.0f );
		GL.Vertex3( 0.0f, 1.0f, 0.0f );
		GL.Vertex3( 1.0f, 0.0f, 0.0f );
		GL.Vertex3( 1.0f, 1.0f, 0.0f );
		GL.End();
		//GL.PopMatrix();
	}
	
	// Update is called once per frame
	void Update () {
		OnPostRender ();
	}
}
