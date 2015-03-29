using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorsPreloadScreen : PreloadScreen
{
	public Image TopDoor;
	public Image BottomDoor;
	public Image Background;
	public Image Logo;
	public Image Loader;
	public Image LoadingProgress;
	public Action onAnimationCompleted;
	public Action onAnimationFinishCompleted;
	public static PreloadScreen instance;
	
	public int topOffset = -10;
	public int bottomOffset = 47;
	public int topOffsetHide = -10;
	public int bottomOffsetHide = 47;


	float OpenAnimationTime = 0.5f;
	float CloseAnimationTime = 0.4f;



	protected override void _StartAnimation()
	{

		//Debug.Log ("aa");
			transform.parent = null;
			DontDestroyOnLoad(transform.gameObject);
			//transform.localPosition = new Vector3(20000, 20000, 20000);



			HideProgress();

			TweenAlpha.Begin(Background.gameObject, CloseAnimationTime, 1).from = 0;
			TweenAlpha.Begin(Logo.gameObject, CloseAnimationTime, 1).from = 0;
			TweenPosition tweenPositionTop = TweenPosition.Begin(TopDoor.gameObject, CloseAnimationTime, new Vector3(0, topOffset));
			TweenPosition tweenPositionBottom = TweenPosition.Begin(BottomDoor.gameObject, CloseAnimationTime, new Vector3(0, bottomOffset));

		tweenPositionTop.from = new Vector3(0, topOffsetHide);
		tweenPositionBottom.from = new Vector3(0, bottomOffsetHide);
			tweenPositionTop.method = tweenPositionBottom.method = UITweener.Method.EaseIn;

			//TweenAlpha.Begin(Loader.gameObject, closeTime, 1).from = 0;

			EventDelegate.Set(tweenPositionBottom.onFinished, delegate
		                  {
			//Debug.Log ("aa");
				if (onAnimationCompleted != null)
				{
					onAnimationCompleted();
				}

				ShowProgress();
				LoadStarted();
			}, true);

			//UIUtils.SetTimeout(EndAnimation,3000,true);


			SetEndTimeout(3f);

	}

	void HideProgress()
	{		
		if (Loader != null) {
			Color c = Loader.color;
			c.a = 0;
			Loader.color = c;
		}
		if (LoadingProgress != null) {
		Color c2 = LoadingProgress.color;
		c2.a = 0;
			LoadingProgress.color = c2;
		}

		SetProgress(0);
	}
	void ShowProgress()
	{
		if (Loader != null && LoadingProgress != null) {
			TweenAlpha.Begin (Loader.gameObject, 0.3f, 1).from = 0;
			TweenAlpha.Begin (LoadingProgress.gameObject, 0.3f, 1).from = 0;
		}
	}

	protected override void _EndAnimation()
	{

		CancelInvoke("EndAnimation");

		//Debug.Log("EndAnimation" + animationStarted);


		if (Loader != null && LoadingProgress != null) {
			TweenAlpha.Begin (Loader.gameObject, OpenAnimationTime, 0);
		}
			TweenAlpha.Begin(Background.gameObject, OpenAnimationTime, 0);
			TweenAlpha.Begin(Logo.gameObject, OpenAnimationTime, 0);
		TweenPosition.Begin(TopDoor.gameObject, OpenAnimationTime, new Vector3(0, topOffsetHide)).method = UITweener.Method.EaseIn;
		TweenPosition tweenPositionBottom = TweenPosition.Begin(BottomDoor.gameObject, OpenAnimationTime, new Vector3(0, bottomOffsetHide));
			tweenPositionBottom.method = UITweener.Method.EaseIn;

			EventDelegate.Set(tweenPositionBottom.onFinished, delegate
			{
				if (onAnimationFinishCompleted != null)
				{
					onAnimationFinishCompleted();
				}
				LoadFinished();
			}, true);


	}

	protected override void _SetProgress(float progress)
	{
		if (LoadingProgress != null)
		{
			LoadingProgress.fillAmount = progress;
		}
	}
}
