using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
	public List<Side> sides;
//	HashSet<> 

	void Awake()
	{
		foreach (var side in sides)
		{
			side.SetBox(this);
		}
	}
}
