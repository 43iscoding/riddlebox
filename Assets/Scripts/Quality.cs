using UnityEngine;
using System.Collections;

public class Quality : MonoBehaviour
{
	void Start ()
	{
		Object.Destroy(GetComponent<UnityStandardAssets.ImageEffects.Antialiasing>());
	}
}
