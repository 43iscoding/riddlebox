using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class Utils
{
	//For float == 0
	public const float Epsilon = 1e-4f;
	static Dictionary<string, string> webArguments;

	public static string ToRoman(int number)
	{
		if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
		if (number < 1) return String.Empty;
		if (number >= 1000) return "M" + ToRoman(number - 1000);
		if (number >= 900) return "CM" + ToRoman(number - 900);
		if (number >= 500) return "D" + ToRoman(number - 500);
		if (number >= 400) return "CD" + ToRoman(number - 400);
		if (number >= 100) return "C" + ToRoman(number - 100);
		if (number >= 90) return "XC" + ToRoman(number - 90);
		if (number >= 50) return "L" + ToRoman(number - 50);
		if (number >= 40) return "XL" + ToRoman(number - 40);
		if (number >= 10) return "X" + ToRoman(number - 10);
		if (number >= 9) return "IX" + ToRoman(number - 9);
		if (number >= 5) return "V" + ToRoman(number - 5);
		if (number >= 4) return "IV" + ToRoman(number - 4);
		if (number >= 1) return "I" + ToRoman(number - 1);
		throw new ArgumentOutOfRangeException("something bad happened");
	}

	public static string Multiply(this string source, int multiplier)
	{
		StringBuilder sb = new StringBuilder(multiplier * source.Length);
		for (int i = 0; i < multiplier; i++)
		{
			sb.Append(source);
		}

		return sb.ToString();
	}

	public static bool IsPaused()
	{
		return Math.Abs(Time.timeScale) < Epsilon;
	}

	public static void ToggleFullscreen()
	{
		SetFullscreen(!IsFullscreen());
	}

	public static void SetFullscreen(bool isFullscreen)
	{
#if !UNITY_FLASH
		if (isFullscreen == Screen.fullScreen)
		{
			return;
		}
		Debug.Log("Set full screen to: " + isFullscreen);
		var res = Screen.currentResolution;

		//http://docs.unity3d.com/Documentation/ScriptReference/Screen-fullScreen.html
		if (isFullscreen)
		{
			Screen.SetResolution(res.width, res.height, isFullscreen);
		}
		else
		{
			Screen.fullScreen = isFullscreen;
		}
		/*if (isFullscreen != Screen.fullScreen)
		{
			Debug.LogWarning("Fullscreen is not supported or is not working!");
			GlobalSetup.allowFullScreen = false;
		}*/
#endif
	}

	public static bool IsFullscreen()
	{
		//Debug.Log("Getting fullscreeen: " + Screen.fullScreen);
		return Screen.fullScreen;
	}

	public static bool BackButtonWasPressed()
	{
		return GetKeyDown(KeyCode.Escape);
	}

	public static Vector2 PixelToScreen(Vector2 screenPosition)
	{
		return new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
	}


	public static string GetDeviceType(bool includeResolution = false)
	{
#if UNITY_FLASH
		return "Flash";
#elif UNITY_IPHONE
		return iPhone.generation.ToString();
#elif UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
		return SystemInfo.deviceModel;
#else
		// PC, Web
		string deviceName = String.Format("{0} {1}", SystemInfo.processorType, SystemInfo.graphicsDeviceName);
		if (includeResolution)
		{
			deviceName += String.Format(" {0}x{1}", Screen.width, Screen.height);
		}
		return deviceName;
#endif
	}

	public static string GetDeviceName()
	{
#if UNITY_EDITOR
		return SystemInfo.deviceName;
#elif UNITY_FLASH
		return "Flash";
#elif UNITY_IPHONE
		return iPhone.generation.ToString();
#elif UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
		return SystemInfo.deviceModel;
#else
		return SystemInfo.deviceName;
#endif
	}



	public static void AppendSemicolon(this StringBuilder sb, string str)
	{
		sb.Append(str);
		sb.Append(";");
	}

	public static string ToMb(long bytes)
	{
		double mb = bytes / 1024.0 / 1024.0;
		if (mb >= 10)
		{
			return mb.ToString("0");
		}
		else
		{
			return mb.ToString("0.#");
		}
	}

	public static string NormSigned(float value)
	{
		if (value > Epsilon)
		{
			return value.ToString("+0.###");
		}
		else if (value < -Epsilon)
		{
			return value.ToString("0.###");
		}
		else
		{
			return "0";
		}
	}

	public static bool GetKeyDown(KeyCode keyCode, bool control = false, bool alt = false, bool shift = false)
	{
		return
			(control == (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) &&
			(alt == (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.AltGr))) &&
			(shift == (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) &&
			Input.GetKeyDown(keyCode);
	}

#if !UNITY_FLASH && !UNITY_METRO
	public static string JoinPath(params string[] path)
	{
		return String.Join(Path.DirectorySeparatorChar.ToString(), path);
	}
#endif

	public static float RoundForUI(float f)
	{
		return Mathf.Round(f * 10) / 10;
	}

	public static T GetRandomEnum<T>()
	{
		Array A = Enum.GetValues(typeof(T));
		T V = (T)A.GetValue(Random.Range(0, A.Length));
		return V;
	}

	//new default function
	public static void OpenURL(string url)
	{
#if UNITY_WEBPLAYER
		if (!Application.isWebPlayer)
		{
			Application.OpenURL(url);
		}
		else
		{
			Application.OpenURL(url);
			//Application.ExternalEval("var windowForLink = window.open('" + url + "','_blank');windowForLink.focus();");
		}
#else
		Application.OpenURL(url);
#endif
	}

	public static void OpenUrl(string url, int w, int h, bool canEmbed = true)
	{
		url += "&width=" + w + "&height=" + h;

#if UNITY_WEBPLAYER
		if (!Application.isWebPlayer)
		{
			Debug.Log(url);
			Application.OpenURL(url);
		}
		else
		{
			TextAsset textAsset = Resources.Load<TextAsset>("OpenExternalURL.txt");
			if (textAsset == null)
			{
				Debug.LogError("No OpenExternalURL.txt");
				return;
			}
			string text = textAsset.text;
			text = text.Replace("%URL%", url);
			text = text.Replace("w = 740;", "w = " + w + ";");
			text = text.Replace("h = 640;", "h = " + h + ";");
			if (!canEmbed)
			{
				text = text.Replace("var canEmbed = true;", "var canEmbed = false;");
			}
			Application.ExternalEval(text);
		}

#else
		Application.OpenURL(url);
#endif
	}

	public static void SetTimeScale(float scale)
	{
		Time.timeScale = Mathf.Clamp(scale, 0, 100);
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static DateTime UnixStartTime
	{
		get { return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); }
	}

	public static string ToBase64String(byte[] getBytes)
	{
#if UNITY_FLASH
		Base64Encoder encoder = new Base64Encoder(getBytes);
		return new string(encoder.GetEncoded());
#else
		return Convert.ToBase64String(getBytes);
#endif
	}

	public static byte[] FromBase64String(string encrypted)
	{
#if UNITY_FLASH
		Base64Decoder decoder = new Base64Decoder(encrypted.ToCharArray());
		return decoder.GetDecoded();
#else
		return Convert.FromBase64String(encrypted);
#endif
	}

	public static byte[] UTF8GetBytes(string text)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		return System.Text.Encoding.UTF8.GetBytes(text);
		//return new byte[0];
#else
		return Encoding.UTF8.GetBytes(text);
#endif
	}

	public static byte[] UTF8GetBytes(char[] text)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		return UTF8GetBytes(new string(text));
#else
		return Encoding.UTF8.GetBytes(text);
#endif
	}

	public static string[] Split(string srcValue, char c)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		return FlashNative.split(srcValue, new string(new []{c}));
#else
		return srcValue.Split(c);
#endif
	}

	public static string[] Split(string srcValue, char c, int count)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		return FlashNative.splitWithLimit(srcValue, new string(new []{c}), count);
#else
		return srcValue.Split(new[] { c }, count);
#endif
	}

	public static byte[] HexToByteArray(string hex)
	{
		if (hex.Length % 2 == 1)
		{
			throw new Exception("The binary key cannot have an odd number of digits");
		}

		byte[] arr = new byte[hex.Length >> 1];

		for (int i = 0; i < (hex.Length >> 1); ++i)
		{
			arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
		}

		return arr;
	}

	public static int GetHexVal(char hex)
	{
		int val = (int)hex;
		//For uppercase A-F letters:
		//return val - (val < 58 ? 48 : 55);
		//For lowercase a-f letters:
		//return val - (val < 58 ? 48 : 87);
		//Or the two combined, but a bit slower:
		return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
	}

	public static void ExternalEval(string funcName, string param)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		FlashNative.ExternalEval(funcName, param);
#elif !UNITY_WP8 && !UNITY_METRO
		Application.ExternalEval(funcName + "('" + param + "')");
#endif
	}

	public static void ExternalEval(string funcName, string param1, string param2, string param3)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		FlashNative.ExternalEval3(funcName, param1, param2, param3);
#elif !UNITY_WP8 && !UNITY_METRO
		Application.ExternalEval(funcName + "('" + param1 + "','" + param2 + "','" + param3 + "')");
#endif
	}

	public static void ExternalEval(string funcName)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		FlashNative.ExternalEvalNoparam(funcName);
#elif !UNITY_WP8 && !UNITY_METRO
		Application.ExternalEval(funcName + "()");
#endif
	}


	public static string EncodeNonAsciiCharacters(string value)
	{
		StringBuilder sb = new StringBuilder();
		foreach (char c in value)
		{
			if (c > 127)
			{
				// This character is too big for ASCII
				string encodedValue = "\\u" + ((int)c).ToString("x4");
				sb.Append(encodedValue);
			}
			else
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}

	public static string DecodeEncodedNonAsciiCharacters(string value)
	{
#if UNITY_FLASH && !UNITY_EDITOR
		return FlashNative.DecodeEncodedNonAsciiCharacters(value);
#else
		return Regex.Replace(
							 value,
			@"\\u(?<Value>[a-zA-Z0-9]{4})",
			m =>
			{
				return ((char)Int32.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
			});
#endif
	}

	public static string GetString(byte[] responseBytes)
	{
		return Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
	}

	public static StringBuilder AppendLine(StringBuilder sb, string s)
	{
#if UNITY_FLASH
		return sb.Append(s).Append("\n");
#else
		return sb.AppendLine(s);
#endif
	}

	public static int LastIndexOfAny(string str, char[] c, int startIndex)
	{
#if UNITY_FLASH
		for (int i = str.Length - 1; i >= startIndex; i--)
		{
			foreach (var cc in c)
			{
				if (str[i] == cc)
				{
					return i;
				}
			}
		}
		return -1;
#else
		return str.LastIndexOfAny(c, startIndex);
#endif
	}

	public static int IndexOfAny(string str, char[] c, int startIndex)
	{
#if UNITY_FLASH
		for (int i = startIndex; i < str.Length; i++)
		{
			foreach (var cc in c)
			{
				if (str[i] == cc)
				{
					return i;
				}
			}
		}
		return -1;
#else
		return str.IndexOfAny(c, startIndex);
#endif
	}

	public static string Format00(int minutes)
	{
#if UNITY_FLASH
		var result = string.Format("{0:00}", minutes);
		if (result.Length < 2)
		{
			result = "0" + result;
		}

		return result;
#else
		return String.Format("{0:00}", minutes);
#endif
	}

	public static string ByteArrayToHexString(byte[] Bytes)
	{
		StringBuilder Result = new StringBuilder(Bytes.Length * 2);
		string HexAlphabet = "0123456789abcdef";

		foreach (byte B in Bytes)
		{
			Result.Append(HexAlphabet[(int)(B >> 4)]);
			Result.Append(HexAlphabet[(int)(B & 0xF)]);
		}

		return Result.ToString();
	}
	
	public static void LimitLength(string s, int max)
	{
		if (s.Length > max)
		{
			s.Substring(0, max);
		}
	}

	public static GameObject GetRandomArrayElement(GameObject[] arr)
	{
		if (arr == null)
		{
			Debug.LogError("Empty array");
			return null;
		}
		return GetArrayElement(arr, Random.Range(0, arr.Length), assert: true);
	}

	public static GameObject GetRandomListElement(List<GameObject> arr)
	{
		if (arr == null)
		{
			Debug.LogError("Empty array");
			return null;
		}
		return GetListElement(arr, Random.Range(0, arr.Count), assert: true);
	}

	public static GameObject GetArrayElement(GameObject[] arr, int index, bool assert)
	{
		if (arr == null || arr.Length == 0)
		{
			if (assert)
			{
				Debug.LogError("Empty array");
			}
			return null;
		}
		if (index >= arr.Length)
		{
			if (assert)
			{
				Debug.LogError("Index "+ index+" is greater than array length: " + arr.Length);
			}
			return null;
		}
		if (index < 0)
		{
			if (assert)
			{
				Debug.LogError("Index is negative: " + index);
			}
			return null;
		}

		return arr[index];
	}

	public static GameObject GetListElement(List<GameObject> arr, int index, bool assert)
	{
		if (arr == null || arr.Count == 0)
		{
			if (assert)
			{
				Debug.LogError("Empty array");
			}
			return null;
		}
		if (index >= arr.Count)
		{
			if (assert)
			{
				Debug.LogError("Index " + index + " is greater than array length: " + arr.Count);
			}
			return null;
		}
		if (index < 0)
		{
			if (assert)
			{
				Debug.LogError("Index is negative: " + index);
			}
			return null;
		}

		return arr[index];
	}

	public static Texture2D GetArrayElement(Texture2D[] arr, int index, bool assert)
	{
		if (arr == null || arr.Length == 0)
		{
			if (assert)
			{
				Debug.LogError("Empty array");
			}
			return null;
		}
		if (index >= arr.Length)
		{
			if (assert)
			{
				Debug.LogError("Index is greater than array length");
			}
			return null;
		}
		if (index < 0)
		{
			if (assert)
			{
				Debug.LogError("Index is negative");
			}
			return null;
		}

		return arr[index];
	}
}
