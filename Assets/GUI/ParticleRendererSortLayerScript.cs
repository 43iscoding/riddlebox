using UnityEngine;

[ExecuteInEditMode]
public class ParticleRendererSortLayerScript : MonoBehaviour
{
	public string sortingLayerName;
	public int sortingOrder;

	void Start()
	{
		//Change Foreground to the layer you want it to display on
		//You could prob. make a public variable for this
		var renderer1 = GetComponent<ParticleRenderer>().GetComponent<Renderer>();
		renderer1.sortingLayerName = sortingLayerName;
		renderer1.sortingOrder = sortingOrder;
	}
}
