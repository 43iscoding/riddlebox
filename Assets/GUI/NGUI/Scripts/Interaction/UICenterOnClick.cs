//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Attaching this script to an element of a scroll view will make it possible to center on it by clicking on it.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Center Scroll View on Click")]
public class UICenterOnClick : MonoBehaviour
{
	public UICenterOnChild center;
	public UIPanel panel;
	
	//sd - additional functions
	void Start()
	{
		center = NGUITools.FindInParents<UICenterOnChild>(gameObject);
		panel = NGUITools.FindInParents<UIPanel>(gameObject);
	}

	void OnClick ()
	{
		Center(false);
	}

	public void Center(bool instant=false)
	{
		if (center != null)
		{
			if (center.enabled)
				center.CenterOn(transform);
		}
		else if (panel != null && panel.clipping != UIDrawCall.Clipping.None)
		{
			UIScrollView sv = panel.GetComponent<UIScrollView>();
			Vector3 offset = -panel.cachedTransform.InverseTransformPoint(transform.position);
			if (!sv.canMoveHorizontally) offset.x = panel.cachedTransform.localPosition.x;
			if (!sv.canMoveVertically) offset.y = panel.cachedTransform.localPosition.y;

			
			if (instant)
			{
				SpringPanel.Begin(panel.cachedGameObject, offset, 10000000);
			} else
			{
				SpringPanel.Begin(panel.cachedGameObject, offset, 6f);
			}

		}
	}
}
