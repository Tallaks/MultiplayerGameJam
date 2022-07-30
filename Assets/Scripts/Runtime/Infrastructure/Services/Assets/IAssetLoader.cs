using MGJ.Runtime.Infrastructure.DI;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.Services.Assets
{
	public interface IAssetLoader : IService
	{
		T LoadFromResources<T>(string path) where T : Object;
	}
}