using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class LabelFontStyle : MonoBehaviour
{
	public UILabel sample;
	UILabel label;

	// Use this for initialization
	void Awake()
	{
		label = GetComponent<UILabel>();
		ApplyStyle();
	}


	void ApplyStyle()
	{

		if (GetComponents<LabelFontStyle>().Length > 1)
		{
			DestroyImmediate(this);
			return;
		}

		if (GetComponents<UILabel>().Length == 0)
		{
			DestroyImmediate(this);
			return;
		}


		if (label != null)
		{
			if (sample!=null)
			{
				label.color = sample.color;
				label.spacingX = sample.spacingX;
				label.spacingY = sample.spacingY;

				label.applyGradient = sample.applyGradient;
				//sd
				label.diagonalGradient = sample.diagonalGradient;
				label.gradientBottom = sample.gradientBottom;
				label.gradientTop = sample.gradientTop;

				label.symbolStyle = sample.symbolStyle;
				label.effectStyle = sample.effectStyle;
				if (label.effectStyle!=UILabel.Effect.None)
				{
					label.effectDistance = sample.effectDistance;
					label.effectColor = sample.effectColor;
					label.effectColorBottom = sample.effectColorBottom;
				}
				sample = null;
			}
		}
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Application.isEditor && !Application.isPlaying)
		{
			ApplyStyle();
		}
	}
#endif
}
