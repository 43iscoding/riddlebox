using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class UIUtils
{
	public enum NguiButtonState
	{
		Normal,
		Hover,
		Press,
		Disabled
	}

	//dlja Ngui animacij//Dlja Ngui knopok

	public static void AnimateGhost(GameObject icon, float scale = 2, float animTime = 0.5f, GameObject iconGhost = null)
	{
		//Debug.Log("Animating Ghost");
		if (iconGhost == null)
		{
			iconGhost = NGUITools.AddChild(icon.transform.parent.gameObject, icon.gameObject);
		}
		if (iconGhost.GetComponent<UIWidget>() == null)
		{
			iconGhost.AddComponent<UIPanel>();
		}

		if (iconGhost.GetComponent<Collider>() != null)
		{
			iconGhost.GetComponent<Collider>().enabled = false;
		}

		ParticleSystem successParticles = icon.GetComponent<ParticleSystem>();
		if (successParticles != null)
		{
			successParticles.gameObject.SetActive(true);
			successParticles.Play();
			successParticles.loop = false;
		}

		iconGhost.transform.localPosition = icon.transform.localPosition;
		iconGhost.transform.localScale = icon.transform.localScale;
		TweenScale tweenScale = TweenScale.Begin(iconGhost, animTime, iconGhost.transform.localScale * scale);
		tweenScale.method = UITweener.Method.EaseOut;
		EventDelegate.Set(tweenScale.onFinished, delegate { NGUITools.Destroy(UITweener.current.gameObject); }, true);

			TweenAlpha tweenAlpha = TweenAlpha.Begin(iconGhost, animTime, 0);
			tweenAlpha.@from = 0.8f;
			tweenAlpha.method = UITweener.Method.EaseOut;

	}

	//Dlja Ngui knopok

	public static void SetEnabled(UIButton button, bool enabled)
	{
		SetEnabled(button.transform, enabled);
	}

	public static void SetEnabled(Transform button, bool enabled)
	{
		UISlider uiSlider = button.GetComponent<UISlider>();
		if (uiSlider != null)
		{
			//uiSlider.enabled = enabled;
			Transform transform = uiSlider.thumb;
			if (transform != null)
			{
				NGUITools.SetActive(transform.gameObject, enabled);
			}
		}
		UIImageButton imageButton = button.GetComponent<UIImageButton>();
		if (imageButton != null)
		{
			imageButton.isEnabled = enabled;
			if (imageButton.target == null)
			{
				Debug.LogWarning("Image button with no target:" + imageButton.name);
			}
			else
			{
				if (enabled)
				{
					imageButton.target.spriteName = imageButton.normalSprite;
				}
				else
				{
					imageButton.target.spriteName = imageButton.disabledSprite;
				}
			}
		}

		UIButton uiButton = button.GetComponent<UIButton>();
		if (uiButton != null)
		{
			uiButton.isEnabled = enabled;
			/*
			Debug.LogWarning("Disabled color:" + uiButton.name + " : " + uiButton.disabledColor);
			if (uiButton.tweenTarget != null)
			{
				UIWidget uiSprite = uiButton.tweenTarget.GetComponent<UIWidget>();
				if (uiSprite != null)
				{
					if (enabled)
					{
						uiSprite.color = uiButton.defaultColor;
					}
					else
					{
						uiSprite.color = uiButton.disabledColor;
					}
				}
			}
			 */
		}

		if (enabled)
		{
			UIButtonColor uiButtonColor = button.GetComponent<UIButtonColor>();
			if (uiButtonColor != null)
			{
				GameObject tweenTarget = uiButtonColor.tweenTarget;
				if (tweenTarget != null)
				{
					if (tweenTarget.GetComponent<TweenColor>() != null)
					{
						tweenTarget.GetComponent<TweenColor>().enabled = false;
						NGUITools.Destroy(tweenTarget.GetComponent<TweenColor>());
					}
					if (tweenTarget.GetComponent<UISprite>() != null)
					{
						tweenTarget.GetComponent<UISprite>().color = uiButtonColor.defaultColor;
					}
					if (tweenTarget.GetComponent<UILabel>() != null)
					{
						tweenTarget.GetComponent<UILabel>().color = uiButtonColor.defaultColor;
					}
					if (tweenTarget.GetComponent<UITexture>() != null)
					{
						tweenTarget.GetComponent<UITexture>().color = uiButtonColor.defaultColor;
					}
				}
			}
		}
		/*UIButtonScale buttonScale = button.GetComponent<UIButtonScale>();
		if (buttonScale != null)
		{
			buttonScale.UpdateColor(enabled, true);
		}*/
		if (button.GetComponent<Collider>() != null)
		{
			button.GetComponent<Collider>().enabled = enabled;
		}
		else
		{
			if (button.GetComponent<BoxCollider2D>() != null)
			{
				button.GetComponent<BoxCollider2D>().enabled = enabled;
			}
		}
	}

	public static void Localize(Transform button, string key)
	{
		UILabel uiLabel = button.GetComponent<UILabel>();
		if (uiLabel == null)
		{
			uiLabel = button.GetComponentInChildren<UILabel>();
		}

		if (uiLabel != null)
		{
			if (uiLabel.GetComponent<UILocalize>() != null)
			{
				Object.Destroy(uiLabel.GetComponent<UILocalize>());
			}
			UILocalize localize = uiLabel.gameObject.AddComponent<UILocalize>();
			localize.key = key;
			localize.Localize();
		}
	}

	public static void SetState(Transform button, NguiButtonState state)
	{
		bool colliderEnabled = false;
		if (button.GetComponent<Collider>() != null)
		{
			colliderEnabled = button.GetComponent<Collider>().enabled;
			button.GetComponent<Collider>().enabled = true;
		}


		switch (state)
		{
			case NguiButtonState.Normal:
				foreach (UIImageButton imageButton in button.GetComponents<UIImageButton>())
				{
					if (imageButton != null)
					{
						imageButton.target.spriteName = imageButton.normalSprite;
					}
				}
				foreach (UIButton uiButton in button.GetComponents<UIButton>())
				{
					if (uiButton != null)
					{
						uiButton.SetNewState(UIButtonColor.State.Normal);
					}
				}
				break;
			case NguiButtonState.Hover:
				foreach (UIImageButton imageButton in button.GetComponents<UIImageButton>())
				{
					if (imageButton != null)
					{
						imageButton.target.spriteName = imageButton.hoverSprite;
					}
				}
				foreach (UIButton uiButton in button.GetComponents<UIButton>())
				{
					if (uiButton != null)
					{
						uiButton.SetNewState(UIButtonColor.State.Hover);
						//uiButton.OnHover(true);
					}
				}
				break;
			case NguiButtonState.Press:
				foreach (UIImageButton imageButton in button.GetComponents<UIImageButton>())
				{
					if (imageButton != null)
					{
						imageButton.target.spriteName = imageButton.pressedSprite;
					}
				}
				foreach (UIButton uiButton in button.GetComponents<UIButton>())
				{
					if (uiButton != null)
					{
						uiButton.SetNewState(UIButtonColor.State.Pressed);
					}
				}
				break;
			case NguiButtonState.Disabled:
				foreach (UIImageButton imageButton in button.GetComponents<UIImageButton>())
				{
					if (imageButton != null)
					{
						imageButton.target.spriteName = imageButton.disabledSprite;
					}
				}
				foreach (UIButton uiButton in button.GetComponents<UIButton>())
				{
					if (uiButton != null)
					{
						uiButton.SetNewState(UIButtonColor.State.Disabled);
					}
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("state");
		}
		if (button.GetComponent<Collider>() != null)
		{
			button.GetComponent<Collider>().enabled = colliderEnabled;
		}
	}

	public static void Localize(UILabel label, string key)
	{
		Localize(label.transform, key);
	}

	public static void SetTimeout(Action callBack, int i)
	{
		//TODO: ochenj nuzhna eta funkcija v normaljnom variante, help :(
		//funkcija vipolnjajet action startLoading cherez i millisekund
		float seconds = i / 1000f;
		GameObject gameObject = new GameObject("TimeoutScale" + i);
		TweenScale comp = TweenScale.Begin(gameObject, seconds, Vector3.one);
		EventDelegate.Set(comp.onFinished, () => {
			Object.Destroy(gameObject);
			callBack();
		}, true);
	}
}
