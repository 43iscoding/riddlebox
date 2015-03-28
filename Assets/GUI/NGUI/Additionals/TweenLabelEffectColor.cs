//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's color.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Label Effect Color")]
public class TweenLabelEffectColor : UITweener
{
	public Color from = Color.white;
	public Color to = Color.white;

	bool mCached = false;
	UILabel mLabel;

	void Cache()
	{
		mCached = true;
		mLabel = GetComponent<UILabel>();
	}

	[System.Obsolete("Use 'value' instead")]
	public Color color { get { return this.value; } set { this.value = value; } }

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public Color value
	{
		get
		{
			if (!mCached) Cache();
			if (mLabel != null) return mLabel.effectColor;
			return Color.black;
		}
		set
		{
			if (!mCached) Cache();
			if (mLabel != null) mLabel.effectColor = value;
		}
	}

	/// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate(float factor, bool isFinished) { value = Color.Lerp(from, to, factor); }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenLabelEffectColor Begin(GameObject go, float duration, Color color)
	{
#if UNITY_EDITOR
		if (!Application.isPlaying) return null;
#endif
		TweenLabelEffectColor comp = UITweener.Begin<TweenLabelEffectColor>(go, duration);
		comp.from = comp.value;
		comp.to = color;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			//sd:4tobi hotj 1 update bil!
			//comp.enabled = false;
		}
		return comp;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue() { from = value; }

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue() { to = value; }

	[ContextMenu("Assume value of 'From'")]
	void SetCurrentValueToStart() { value = from; }

	[ContextMenu("Assume value of 'To'")]
	void SetCurrentValueToEnd() { value = to; }
}
