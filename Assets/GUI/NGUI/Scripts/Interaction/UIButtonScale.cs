//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Simple example script of how a button can be scaled visibly when the mouse hovers over it or it gets pressed.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	public Transform tweenTarget;
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);
	public float duration = 0.2f;

	Vector3 mScale;
	bool mStarted = false;

	void Start ()
	{
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null) tweenTarget = transform;
			//sd: iw in tweenig process - take the final tween
			TweenScale tweenScale = GetComponent<TweenScale>();
			if (tweenScale != null && tweenScale.enabled)
			{
				mScale = tweenScale.to;
			}
			else
			{
				mScale = tweenTarget.localScale;
			}



#if UNITY_4_6
			/*Dictionary<EventTriggerType, Action> listActions = new Dictionary<EventTriggerType, Action>()
			{
			{ EventTriggerType.PointerEnter, () => OnHover(true) },
			{ EventTriggerType.PointerExit, () => OnHover(false) },
			{ EventTriggerType.PointerDown, () => OnPress(true) },
			{ EventTriggerType.PointerUp, () => OnPress(false) },
			{ EventTriggerType.Select, () => OnSelect(true) },
			{ EventTriggerType.Deselect, () => OnSelect(false) }
			};
			AtlasConverter.AddEvents(gameObject, listActions);*/
#endif


		}
	}


    void OnEnable () { if (mStarted) OnHover(UICamera.IsHighlighted(gameObject)); }

	void OnDisable ()
	{
		if (mStarted && tweenTarget != null)
		{
			TweenScale tc = tweenTarget.GetComponent<TweenScale>();

			if (tc != null)
			{
				tc.value = mScale;
				tc.enabled = false;
			}
		}
	}

	void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenScale tweenScale = TweenScale.Begin(tweenTarget.gameObject, duration, isPressed ? Vector3.Scale(mScale, pressed) : (UICamera.IsHighlighted(gameObject) ? Vector3.Scale(mScale, hover) : mScale));
			tweenScale.method = UITweener.Method.EaseInOut;
			//sd:
			tweenScale.delay = 0;
		}
	}

	void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenScale tweenScale = TweenScale.Begin(tweenTarget.gameObject, duration, isOver ? Vector3.Scale(mScale, hover) : mScale);
			tweenScale.method = UITweener.Method.EaseInOut;
			//sd:
			tweenScale.delay = 0;
		}
	}

	void OnSelect (bool isSelected)
	{
		if (enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
			OnHover(isSelected);
	}


}
