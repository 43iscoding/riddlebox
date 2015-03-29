using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rotateSwitch : MonoBehaviour 
{
	static int locks = 0;
	public static bool complete;
	public bool vertical;
	
	public List<GameObject> switchesToTurn;
	public List<GameObject> allSwitches;

	// Use this for initialization
	void Start () {
		vertical = true;
		complete = false;
	}
	
	public void TurnSwitch()
	{
		StartCoroutine(TweenRotate());
	//	transform.RotateAround(transform.position, transform.forward, 90);
		vertical = !vertical;	
	}
	
	IEnumerator TweenRotate()
	{
		locks++;
		Timer t = new Timer(0.3f);
		Quaternion from = transform.localRotation;
		Quaternion to;
		if (vertical)
		{
			to = Quaternion.Euler(0,0,90);
		}
		else
		{
			to = Quaternion.Euler(0,0,0);			
		}
		

		while (!t.IsEnded())
		{
			transform.localRotation = Quaternion.Lerp(from, to, t.GetCompletePercent());
			
			yield return null;
		}
		transform.localRotation =  to;
		locks--;
	}
	
	void OnMouseDown()
	{
		if (locks == 0)
		{	
			TurnSwitch();
			
			for (int i = 0; i < switchesToTurn.Count; i++)
			{
				switchesToTurn[i].GetComponent<rotateSwitch>().TurnSwitch();
			}
			
			rotateSwitch switch1 = allSwitches[1].GetComponent<rotateSwitch>();
			rotateSwitch switch3 = allSwitches[3].GetComponent<rotateSwitch>();
			rotateSwitch switch5 = allSwitches[5].GetComponent<rotateSwitch>();
			rotateSwitch switch7 = allSwitches[7].GetComponent<rotateSwitch>();
					
			if (!switch1.vertical && switch3.vertical && switch5.vertical && !switch7.vertical)
			{
				complete = true;
			    print ("switches complete");
			}
		}
	}
}
