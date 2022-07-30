using UnityEngine;

namespace MGJ.Runtime.Infrastructure.Services.Assets
{
	public class AssetLoader : IAssetLoader
	{
		public T LoadFromResources<T>(string path) where T : Object
		{
			return Resources.Load<T>(path);
		}
	}
}