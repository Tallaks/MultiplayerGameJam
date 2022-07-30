using System;
using MGJ.Runtime.Infrastructure.DI;
using UnityEngine.SceneManagement;

namespace MGJ.Runtime.Infrastructure.Services.SceneManagement
{
	public interface ISceneLoader : IService
	{
		void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single, Action onLoad = null);
		void UnloadScene(string name, UnloadSceneOptions unloadOptions = UnloadSceneOptions.None, Action onUnload = null);
	}
}