using UnityEngine;
using System.Collections;
	
public class roatateSwitch : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			//GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 0, 90));
			GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, 0, 10f * Input.GetAxis("Mouse X")+ 10f * Input.GetAxis("Mouse Y")));
		}		
	}

}