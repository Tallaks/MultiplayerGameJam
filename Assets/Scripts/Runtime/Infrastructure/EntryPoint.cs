using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services;
using MGJ.Runtime.Infrastructure.Services.Assets;
using MGJ.Runtime.Infrastructure.Services.Coroutines;
using MGJ.Runtime.Infrastructure.Services.GameObjects;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure
{
	public class EntryPoint : MonoBehaviour
	{
		private void Awake()
		{
			Debug.Log("Preparing Services...");
			BindServices();
			Debug.Log("Services prepared");
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
		}
	}
}