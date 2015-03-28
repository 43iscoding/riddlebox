using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SideWithFourRotatingHandles : Side
{
    public List<GameObject> handles;
	public List<GameObject> lightBulbs;

	public Texture greenLight;

    public void Start()
    {

    }

    public void Update()
    {
        var areAllOk = true;
        foreach (var h in handles)
        {
            if (!h.GetComponent<Handle>().IsOk())
            {
                areAllOk = false;
            }
        }

        //if (areAllOk)
        if (areAllOk || !areAllOk) // TESTING LINE
        {
            foreach(var l in lightBulbs) // here will be lightbulbs
            {
                //l.GetComponent<Renderer>().material.color = Color.green;
				l.GetComponent<Renderer>().materials[1].SetTexture(1, greenLight);
            }
        }
    }
}