using UnityEngine;
using System.Collections;


public class RotateSkybox : MonoBehaviour
{
	public float speed=2;
	Skybox skybox;


	void Awake(){
		skybox = GetComponent<Skybox> ();
	}

	void Update()
	{
		// Construct a rotation matrix and set it for the shader
		float m = (-Time.realtimeSinceStartup*speed % 360);
		skybox.material.SetFloat("_Rotation", m);
	}
}
