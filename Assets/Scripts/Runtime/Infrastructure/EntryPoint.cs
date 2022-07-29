using MGJ.Runtime.Infrastructure.DI;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure
{
	public class EntryPoint : MonoBehaviour
	{
		private void Awake()
		{
			Container.Services.Bind<ITestService>().To(() => new TestService());
			Container.Services.Resolve<ITestService>().Test();
		}
	}
}