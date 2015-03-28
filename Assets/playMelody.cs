using UnityEngine;
using System.Collections;

public class playMelody : MonoBehaviour
{

	PlayMelodyScript melodyScript;

	// Use this for initialization
	void Start () {
		melodyScript = transform.GetComponent<PlayMelodyScript>();
	}
	
	void OnMouseDown() {
		melodyScript.Play();	
	}
}
