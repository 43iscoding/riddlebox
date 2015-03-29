//----------------------------------------------
//			NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the UISlider value.
/// </summary>

[AddComponentMenu("NGUI/Tween/SliderValue")]
public class TweenSliderValue : UITweener
{
	public float from = 0f;
	public float to = 1f;

	UISlider mUiSlider;
	UISprite mUiFilledSprite;


	/// <summary>
	/// Cached version of 'UISlider', as it's always faster to cache.
	/// </summary>

	public UISlider uiSlider
	{
		get
		{
				if (mUiSlider == null)
				{
					mUiSlider = GetComponentInChildren<UISlider>();

					if (mUiSlider == null)
					{
						Debug.LogError("TweenSliderValue needs an UISlider to work with", this);
						enabled = false;
					}
				}
			
			return mUiSlider;
		}
	}
	public UISprite uiFilledSprite
	{
		get
		{
			if (mUiFilledSprite == null)
				{
					mUiFilledSprite = GetComponentInChildren<UISprite>();

					if (mUiFilledSprite == null)
					{
						Debug.LogError("TweenSliderValue needs an UISprite to work with", this);
						enabled = false;
					}
				}

			return mUiFilledSprite;
		}
	}

	/// <summary>
	/// Audio source's current volume.
	/// </summary>

	public float value 
	{
		get {
			if (mUiSlider != null)
			{
				return mUiSlider.value;
			}
			if (mUiFilledSprite != null)
			{
				return mUiFilledSprite.fillAmount;
			}
			return 0;
		}
		set {
			if (mUiSlider != null)
			{

				uiSlider.value = value;
			}
			if (mUiFilledSprite != null)
			{
				uiFilledSprite.fillAmount = value;
			}
		} 
	}

	/// <summary>
	/// Tween update function.
	/// </summary>

	override protected void OnUpdate(float factor, bool isFinished)
	{

		if (mUiSlider != null)
		{
			value = from * (1f - factor) + to * factor;
			mUiSlider.enabled = (mUiSlider.value > 0.01f);
		}
		if (mUiFilledSprite != null)
		{
			value = from * (1f - factor) + to * factor;
			mUiFilledSprite.enabled = (mUiFilledSprite.fillAmount > 0.01f);
		}

	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenSliderValue Begin(GameObject go, float duration, float targetSliderValue, GameObject eventReceiver = null, string callWhenFinished = null)
	{
		TweenSliderValue comp = Begin<TweenSliderValue>(go, duration);


		comp.mUiSlider = go.GetComponentInChildren<UISlider>();
		comp.mUiFilledSprite = go.GetComponentInChildren<UISprite>();
		comp.from = comp.value;


		comp.to = targetSliderValue;
		if (eventReceiver!=null && callWhenFinished != null)
		{
			comp.eventReceiver = eventReceiver;
			comp.callWhenFinished = callWhenFinished;
		}
		return comp;
	}
}