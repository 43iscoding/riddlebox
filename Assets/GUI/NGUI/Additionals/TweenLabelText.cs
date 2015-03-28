using System;
//using System.Text.RegularExpressions;
using UnityEngine;

public class TweenLabelText : UITweener
{
	public int from;
	public int to;
	public int currentValue=0;
	//private Color startColor;
	UILabel mTrans;
	public string format;

	public UILabel cachedTransform { get { if (mTrans == null) mTrans = transform.GetComponent<UILabel>(); return mTrans; } }
	public String text { get { return cachedTransform.text; } set {

		if (format != null)
		{
			cachedTransform.text = string.Format(format,value);
		}
		else
		{
			cachedTransform.text = value; 
		}
	} }
	//public Color color { get { return cachedTransform.color; } set { cachedTransform.color = value; } }

	override protected void OnUpdate(float factor, bool isFinished)
	{

		currentValue = (int)Mathf.Lerp(from,to,factor);
		text = currentValue.ToString();
		

		
		/*
		float colorFactor = 1f - Math.Abs(factor - 0.5f) * 2;

		cachedTransform.effectDistance = new Vector2((0 * (1f - factor) + 5 * factor), (0 * (1f - factor) + 5 * factor));
		cachedTransform.effectStyle = UILabel.Effect.Outline;
	   


		if (from > to)
		{
			//cachedTransform.color = Color.Lerp(startColor, Color.red, colorFactor);
			cachedTransform.effectColor = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), factor);
		} else
		{
			cachedTransform.effectColor = Color.Lerp(new Color(0, 1, 0, 0.5f), new Color(0, 1, 0, 0), factor);
			cachedTransform.color = Color.Lerp(startColor, Color.green, colorFactor);
		}
		 */
	}

	void Awake()
	{
	   //startColor = color;
	}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenLabelText Begin(GameObject go, float duration, int finalValue)
	{
		TweenLabelText comp = UITweener.Begin<TweenLabelText>(go, duration);

		comp.from = comp.currentValue;
		comp.to = finalValue;
		comp.method=Method.EaseOut;
		

		//comp.startColor=comp.color;
		/*
		float speed = 100; //50/s
		duration = Math.Min(duration,Math.Abs(comp.from - comp.to)/speed);
		comp.duration = duration;
		*/
		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			//sd:4tobi hotj 1 update bil!
			//comp.enabled = false;
		}
		return comp;
	}
}