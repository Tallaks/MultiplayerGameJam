using System;
using System.Collections;
using MGJ.Runtime.Infrastructure.Services.Coroutines;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace MGJ.Runtime.Infrastructure.Services.SceneManagement
{
	public class PhotonSceneLoader : ISceneLoader
	{
		private ICoroutineRunner _coroutineRunner;

		public PhotonSceneLoader(ICoroutineRunner coroutineRunner) => 
			_coroutineRunner = coroutineRunner;

		public void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single, Action onLoad = null)
		{
			PhotonNetwork.LoadLevel(name);
			_coroutineRunner.StartCoroutine(InvokeOnLoadAfterSceneLoadCompletion(onLoad));
		}

		private IEnumerator InvokeOnLoadAfterSceneLoadCompletion(Action onLoad)
		{
			while (PhotonNetwork.LevelLoadingProgress < 1)
				yield return null;
			onLoad?.Invoke();
		}

		public void UnloadScene(string name, UnloadSceneOptions unloadOptions = UnloadSceneOptions.None, Action onUnload = null)
		{
			throw new NotImplementedException();
		}
	}
}