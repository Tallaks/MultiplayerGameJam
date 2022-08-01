using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.SceneManagement;
using UnityEditor;
using UnityEngine;

namespace MGJ.Runtime.UI.MainMenu
{
	public class Mediator : MonoBehaviour
	{
		private ISceneLoader _sceneLoader;

		private void Awake()
		{
			Construct();
		}

		public void EnterLobby() => 
			_sceneLoader.LoadScene("Lobby");

		public void QuitGame()
		{
#if UNITY_EDITOR
			EditorApplication.ExecuteMenuItem("Edit/Play");
#endif
			Application.Quit();
		}

		private void Construct()
		{
			_sceneLoader = Container.Services.Resolve<ISceneLoader>();
		}
	}
}