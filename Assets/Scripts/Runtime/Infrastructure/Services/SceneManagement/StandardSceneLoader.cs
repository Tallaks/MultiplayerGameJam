using System;
using System.Collections;
using MGJ.Runtime.Infrastructure.Services.Coroutines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MGJ.Runtime.Infrastructure.Services.SceneManagement
{
	public class StandardSceneLoader : ISceneLoader
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public StandardSceneLoader(ICoroutineRunner coroutineRunner) => 
			_coroutineRunner = coroutineRunner;

		public void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single, Action onLoad = null) => 
			_coroutineRunner.StartCoroutine(LoadSceneRoutine(name, mode, onLoad));

		public void UnloadScene(string name, UnloadSceneOptions unloadOptions = UnloadSceneOptions.None, Action onUnload = null)
		{
			_coroutineRunner.StartCoroutine(UnloadSceneRoutine(name, unloadOptions, onUnload));
		}

		private IEnumerator LoadSceneRoutine(string name, LoadSceneMode mode, Action onLoad)
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, mode);
			yield return asyncOperation;
			onLoad?.Invoke();
		}

		private IEnumerator UnloadSceneRoutine(string name, UnloadSceneOptions unloadOptions, Action onUnload)
		{
			AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(name, unloadOptions);
			yield return asyncOperation;
			onUnload?.Invoke();
		}
	}
}