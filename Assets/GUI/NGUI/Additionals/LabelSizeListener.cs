using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(UILabel))]
public class LabelSizeListener : MonoBehaviour
{
	public UISprite sprite;
	public int paddingX = 40;
	public int paddingY = 16;
	UILabel buttonLabel;
	// Use this for initialization
	void Start()
	{
		buttonLabel = GetComponent<UILabel>();
		SetSize();
	}

	public void SetSize()
	{
		
		if (sprite!=null && buttonLabel != null)
		{
			SetButtonSize(sprite, buttonLabel, paddingX, paddingY);
		}
	}
	public void SetButtonSize(UISprite buttonBack, UILabel buttonLabel, int paddingX = 40, int paddingY = 16)
	{
		buttonBack.width = buttonLabel.width + paddingX;
		buttonBack.height = buttonLabel.height + paddingY;
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Application.isEditor && !Application.isPlaying)
		{
			SetSize();
		}
	}
	#endif

}
