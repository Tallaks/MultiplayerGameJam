using UnityEngine;

namespace MGJ.Runtime.Infrastructure.Services.GameObjects
{
	public class GameObjectFactory : IGameObjectFactory
	{
		public T Create<T>(T prefab) where T : Object => 
			Object.Instantiate(prefab);

		public T Create<T>(T prefab, Transform parent) where T : Object => 
			Object.Instantiate(prefab, parent);

		public T Create<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object => 
			Object.Instantiate(prefab, position, rotation, parent);
	}
}