using System;
using System.Collections;
using UnityEngine;

#if UNITY_EDITOR && !UNITY_WEBPLAYER
using System.IO;
using UnityEditor;
#endif

public class Screenshot : MonoBehaviour
{

	public int scale = 2;
	const bool EditorScreenshot = false;
	
	// return file name
	string GenerateFilenameForScreenshot(int width, int height)
	{
		return string.Format("Screen_"+Application.loadedLevelName + "_{0}x{1}_{2}.png",
		                     width, height,
		                     DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Utils.GetKeyDown(KeyCode.ScrollLock))
		{
			StartScreenshot();
		}
	}
	
	public void StartScreenshot()
	{
		CaptureScreenshot();
	}

	
	void CaptureScreenshot()
	{

		
		HideRoot();
		
		if (Application.isEditor && EditorScreenshot)
		{
			#if UNITY_EDITOR && !UNITY_WEBPLAYER
			StartCoroutine(UploadPNG());
			#else
			
			string filename = GenerateFilenameForScreenshot(Screen.width, Screen.height);
			//var path = Application.persistentDataPath + "/Snapshots/" + filename;
			string path = "" + filename;
			//string path = "Screenshots/" + filename;
			
			Debug.Log("!Capturing screenshot: " + path);
			Application.CaptureScreenshot(path, scale);
			#endif
		}
		else
		{
			string filename = GenerateFilenameForScreenshot(Screen.width, Screen.height);
			//var path = Application.persistentDataPath + "/Snapshots/" + filename;
			string path = "" + filename;
			//string path = "Screenshots/" + filename;
			
			Debug.Log("!Capturing screenshot: " + path);
			Application.CaptureScreenshot(path, scale);
		}
		
	}
	
	#if UNITY_EDITOR && !UNITY_WEBPLAYER
	IEnumerator UploadPNG()
	{
		yield return new WaitForEndOfFrame();
		
		
		SetScreenWidthAndHeightFromEditorGameViewViaReflection();
		if (editorDesiredScreenWidth == 0 || editorDesiredScreenHeight==0)
		{
			editorDesiredScreenWidth = Screen.width;
			editorDesiredScreenHeight = Screen.height;
		}
		
		Debug.Log(editorDesiredScreenWidth + "x" + editorDesiredScreenHeight);
		
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels (new Rect(0, 0, width, height), 0, 0);
		tex.Apply ();
		
		
		Texture2D newScreenshot = ScaleTexture(tex, editorDesiredScreenWidth, editorDesiredScreenHeight);
		
		// Encode texture into PNG
		byte[] bytes = newScreenshot.EncodeToPNG();
		Destroy (tex);
		Destroy(newScreenshot);
		
		string filename = GenerateFilenameForScreenshot(editorDesiredScreenWidth, editorDesiredScreenHeight);
		
		Debug.Log("!Capturing screenshot: " + filename);
		
		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(filename, bytes);
	}
	
	private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
		float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
		for (int px = 0; px < rpixels.Length; px++)
		{
			rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth),
			                                      incY * ((float)Mathf.Floor(px / targetWidth)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		return result;
	}
	
	int editorDesiredScreenWidth = 0;
	int editorDesiredScreenHeight = 0;
	
	void SetScreenWidthAndHeightFromEditorGameViewViaReflection()
	{
		//Taking game view using the method shown below	
		var gameView = GetMainGameView();
		var prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		var gvsize = prop.GetValue(gameView, new object[0] { });
		var gvSizeType = gvsize.GetType();
		
		//I have 2 instance variable which this function sets:
		editorDesiredScreenHeight = (int)gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
		editorDesiredScreenWidth = (int)gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
	}
	
	UnityEditor.EditorWindow GetMainGameView()
	{
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetMainGameView.Invoke(null, null);
		return (UnityEditor.EditorWindow)Res;
	}
	#endif
	

	
	void HideRoot()
	{
	}
	
	void ShowRoot()
	{
	}
}
