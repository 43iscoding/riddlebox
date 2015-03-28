using UnityEngine;
using System.Collections;


public class RotateSkybox : MonoBehaviour
{
	
	void Update()
	{
		// Construct a rotation matrix and set it for the shader
		float m = (-Time.realtimeSinceStartup*2 % 360);
		Debug.Log(m);
		GetComponent<Skybox>().material.SetFloat("_Rotation", m);
	}

}
