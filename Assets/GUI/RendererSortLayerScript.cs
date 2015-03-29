using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RendererSortLayerScript : MonoBehaviour
{
	public string sortingLayerName="";
	public int sortingOrder=0;
	//public Color materialColor = Color.white;

	void Awake()
	{
		SetSorting();
	}

#if UNITY_EDITOR
	void Update()
	{
		if (!Application.isPlaying)
		{
			SetSorting();
		}
	}
#endif

	void SetSorting()
	{
		Renderer r = GetComponent<Renderer>();
		r.sortingLayerName = sortingLayerName;
		r.sortingOrder = sortingOrder;
		/*
		r.sharedMaterial.SetColor("_Color",materialColor);
		r.sharedMaterial.SetColor("_TintColor", materialColor);
		r.sharedMaterial.SetColor("_MainTex", materialColor);
		 */
	}

}
