using MGJ.Runtime.Infrastructure.DI;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.Services.GameObjects
{
	public interface IGameObjectFactory : IService
	{
		T Create<T>(T prefab) where T : Object;
		T Create<T>(T prefab, Transform parent) where T : Object;
		T Create<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object;
	}
}