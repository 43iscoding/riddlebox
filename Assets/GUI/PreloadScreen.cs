using System;
using System.Collections;
using UnityEngine;

public abstract class PreloadScreen : MonoBehaviour
{
    public static PreloadScreen prefab;
	
	protected static int sceneToLoadIndex = - 1;
    protected static string sceneToLoad = "";
    protected static Action onLoadComplete;

    private static PreloadScreen instance;

    protected bool animationStarted = false;

	bool addictive = false;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public void StartAnimation()
    {
		//Debug.Log("Start animation!");
        if (animationStarted) return;

        animationStarted = true;

        _StartAnimation();
    }
    protected abstract void _StartAnimation();

    public void SetEndTimeout(float seconds)
    {
        Invoke("EndAnimation", seconds);
    }

    public void EndAnimation()
	{
		//Debug.Log("end animation!");
        CancelInvoke("EndAnimation");

        if (!animationStarted) return;
        animationStarted = false;

        _EndAnimation();
    }
    protected abstract void _EndAnimation();

	public static void Load(int sceneIndex, Action onComplete = null, bool add=false)
	{
		onLoadComplete = onComplete;
		sceneToLoadIndex = sceneIndex;
		sceneToLoad = "";

		BeginLoading(add);
	}

	static void BeginLoading(bool add=false)
	{
		if (instance == null && prefab != null) instance = Instantiate(prefab) as PreloadScreen;
		if (instance == null)
		{
			//if we have no prefab - just load what is needed
			LoadStarted();
		}
		else
		{
			instance.addictive = add;
			instance.StartAnimation();
		}
	}

	public static void Load(string scene, Action onComplete = null)
    {
		onLoadComplete = onComplete;
		sceneToLoadIndex = -1;
        sceneToLoad = scene;

		BeginLoading();
    }

    protected static void LoadStarted()
    {
		if (PreloadScreen.instance==null)
		{
			if (sceneToLoadIndex >= 0)
			{
				Application.LoadLevel(sceneToLoadIndex);
			}
			else
			{
				Application.LoadLevel(sceneToLoad);
			}

		}
		else
		{
			if (PreloadScreen.instance.addictive)
			{
				PreloadScreen.instance.StartCoroutine(LoadLevelAddAsync());
			}
			else
			{
				PreloadScreen.instance.StartCoroutine(LoadLevelAsync());
			}
		}
    }


	static IEnumerator LoadLevelAddAsync()
	{
#if UNITY_FLASH
        if (sceneToLoadIndex >= 0)
		{
			Application.LoadLevel(sceneToLoadIndex);
		}
		else
		{
			Application.LoadLevel(sceneToLoad);
		}
        yield break;
#else
		//Application.backgroundLoadingPriority = ThreadPriority.Low;
		AsyncOperation async;
		if (sceneToLoadIndex >= 0)
		{
			async = Application.LoadLevelAdditiveAsync(sceneToLoadIndex);
		}
		else
		{
			async = Application.LoadLevelAdditiveAsync(sceneToLoad);
		}
		async.allowSceneActivation = false;

		if (async == null)
		{
			yield break;
		}
		while (async.progress < 0.9f)
		{
			SetPreloadProgress(async.progress);
			yield return null;
		}
		async.allowSceneActivation = true;
		yield return async;
#endif
	}

	static IEnumerator LoadLevelAsync()
	{
#if UNITY_FLASH
        if (sceneToLoadIndex >= 0)
		{
			Application.LoadLevel(sceneToLoadIndex);
		}
		else
		{
			Application.LoadLevel(sceneToLoad);
		}
        yield break;
#else
		//Application.backgroundLoadingPriority = ThreadPriority.Low;
		AsyncOperation async;
		if (sceneToLoadIndex >= 0)
		{
			async = Application.LoadLevelAsync(sceneToLoadIndex);
		}
		else
		{
			async = Application.LoadLevelAsync(sceneToLoad);
		}

		if (async == null)
		{
			yield break;
		}
		while (async.progress < 0.9f)
		{
			SetPreloadProgress(async.progress);
			yield return null;
		}
		//async.allowSceneActivation = true;
		yield return async;
#endif
	}

	static void SetPreloadProgress(float progress)
	{
		instance.SetProgress(progress);
	}

	public void SetProgress(float progress)
	{
		_SetProgress(progress);
	}
	protected abstract void _SetProgress(float progress);


    protected void OnLevelWasLoaded(int i)
    {
        //EndAnimation();
		UIUtils.SetTimeout(EndAnimation,500,true);
    }

    protected void LoadFinished()
    {
        if (onLoadComplete != null) onLoadComplete();


		//stavim etot prefab v sledujushuju scenu!
//		if (GuiBase.GuiPrefabs != null)
//		{
//			GuiBase.GuiPrefabs.preloadScreenPrefab = prefab;
//		}


	    Destroy(gameObject);
    }
}
