using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Assets;
using MGJ.Runtime.Infrastructure.Services.Coroutines;
using MGJ.Runtime.Infrastructure.Services.GameObjects;
using MGJ.Runtime.Infrastructure.Services.Network;
using MGJ.Runtime.Infrastructure.Services.SceneManagement;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.ApplicationPoints
{
	public class EntryPoint : MonoBehaviour, IApplicationPoint
	{
		private void Awake() => 
			PreparePoint();

		public void PreparePoint()
		{
			Debug.Log("Preparing Services...");
			BindServices();
			Debug.Log("Services prepared");
			
			Debug.Log("Loading next scene...");
			Container.Services.Resolve<ISceneLoader>().LoadScene("MainMenu");	
		}

		private void BindServices()
		{
			Container.Services.
				Bind<IAssetLoader>().
				To<AssetLoader>()
				.FromMethod(() => new AssetLoader());
			
			var assetLoader = Container.Services.Resolve<IAssetLoader>();
			
			Container.Services.
				Bind<IGameObjectFactory>().
				To<GameObjectFactory>().
				FromMethod(() => new GameObjectFactory());
			
			var gameObjectFactory = Container.Services.Resolve<IGameObjectFactory>();

			Container.Services.
				Bind<ICoroutineRunner>().
				To<CoroutineRunner>().
				FromMethod(() => 
					gameObjectFactory.Create(assetLoader.LoadFromResources<GameObject>("CoroutineRunner"))
						.With(o => o.AddComponent<CoroutineRunner>()).GetComponent<CoroutineRunner>());
			
			Container.Services.
				Bind<ISceneLoader>().
				To<StandardSceneLoader>().
				FromMethod(() => new StandardSceneLoader(Container.Services.Resolve<ICoroutineRunner>()));
			
			Container.Services.
				Bind<IConnection>().
				To<PhotonConnectionService>().
				FromMethod(() => gameObjectFactory.Create(assetLoader.LoadFromResources<GameObject>("PhotonConnection"))
					.GetComponent<PhotonConnectionService>());

			Container.Services.
				Bind<ILobby>().
				To<PhotonLobby>().
				FromMethod(FindObjectOfType<PhotonLobby>);
		}
	}
}