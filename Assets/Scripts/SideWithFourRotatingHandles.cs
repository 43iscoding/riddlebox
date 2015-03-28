using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SideWithFourRotatingHandles : Side
{
    public List<GameObject> handles;
    //public List<GameObject> lightBulbs;

    public void Start()
    {

    }

    public void UnUpdate()
    {
        var areAllOk = true;
        foreach (var h in handles)
        {
            if (!h.GetComponent<Handle>().IsOk())
            {
                // TESTING
                h.GetComponent<Renderer>().material.color = Color.green;

                areAllOk = false;
            }
        }
        if (areAllOk)
        {
            //foreach(var l in lightBulbs)
            //{
                    
            //}
            foreach(var l in handles) // here will be lightbulbs
            {
                l.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}
