using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rotateSwitch : MonoBehaviour 
{
	static int locks = 0;
	public bool vertical;
	public bool needVertical;
	public bool ignore;
	
	public List<GameObject> switchesToTurn;

	void Start ()
	{
		SetCorrectRotation();
	}
	
	public void TurnSwitch()
	{
		vertical = !vertical;
		SetCorrectRotation();
	}

	void SetCorrectRotation()
	{
		StartCoroutine(TweenRotate());
	}

	IEnumerator TweenRotate()
	{
		locks++;
		Timer t = new Timer(0.3f);
		Quaternion from = transform.localRotation;
		Quaternion to;
		if (vertical)
		{
			to = Quaternion.Euler(0,-90,0);
		}
		else
		{
			to = Quaternion.Euler(0,-90,90);			
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
		if (locks == 0 && SwitchSide.instance.box.IsUnlocked(SwitchSide.instance.box.sides[0]))
		{
			SoundUtils.PlaySound(SoundManager.instance.unlocking);
			TurnSwitch();
			
			for (int i = 0; i < switchesToTurn.Count; i++)
			{
				switchesToTurn[i].GetComponent<rotateSwitch>().TurnSwitch();
			}
			SwitchSide.instance.Check();
		}
	}

	public bool IsOk()
	{
		return ignore || vertical == needVertical;
	}
}
