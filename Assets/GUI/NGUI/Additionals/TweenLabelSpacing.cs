//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// Tween the UILabel's label spacing.
/// </summary>

[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Tween/Tween Label Spacing")]
public class TweenLabelSpacing : UITweener
{
	public float fromX = 0;
	public float toX = 20;
	public bool tweenX = false;
	public float fromY = 0;
	public float toY = 0;
	public bool tweenY = false;

	UILabel mLabel;

	public UILabel cachedLabel { get { if (mLabel == null) mLabel = GetComponent<UILabel>(); return mLabel; } }


	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (tweenX)
		{
			cachedLabel.spacingX = fromX * (1f - factor) + toX * factor;
		}
		if (tweenY)
		{
			cachedLabel.spacingY = Mathf.RoundToInt(fromY * (1f - factor) + toY * factor);
		}
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenLabelSpacing Begin(UILabel label, float duration, Vector2 newSpacing)
	{
		TweenLabelSpacing comp = UITweener.Begin<TweenLabelSpacing>(label.gameObject, duration);
		comp.fromX = label.spacingX;
		comp.toX = newSpacing.x;
		if (Math.Abs(comp.fromX - comp.toX) > float.Epsilon)
		{
			comp.tweenX = true;
		}

		comp.fromY = label.spacingY;
		comp.toY = newSpacing.y;

		if (Math.Abs(comp.fromY - comp.toY) > float.Epsilon)
		{
			comp.tweenY = true;
		}


		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			//sd:4tobi hotj 1 update bil!
			//comp.enabled = false;
		}
		return comp;
	}
}
