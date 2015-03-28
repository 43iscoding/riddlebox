using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SideWithFourRotatingHandles : Side
{
    public List<GameObject> handles;
    public List<GameObject> lightBulbs;

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
            var greenLight = (Texture2D) Resources.LoadAssetAtPath("Assets/Scenes/Level2/Textures/Green_Light.jpg", typeof(Texture2D));
            foreach(var l in lightBulbs) // here will be lightbulbs
            {
                //l.GetComponent<Renderer>().material.color = Color.green;
                l.GetComponent<Renderer>().materials[1].SetTexture(1, greenLight);
            }
        }
    }
}
