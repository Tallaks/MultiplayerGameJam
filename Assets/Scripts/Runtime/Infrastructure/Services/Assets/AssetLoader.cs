using MGJ.Runtime.Infrastructure.Services.Assets;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure
{
	public class AssetLoader : IAssetLoader
	{
		public T LoadFromResources<T>(string path) where T : Object
		{
			return Resources.Load<T>(path);
		}
	}
}