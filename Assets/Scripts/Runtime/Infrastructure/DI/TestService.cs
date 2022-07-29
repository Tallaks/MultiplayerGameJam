using UnityEngine;

namespace MGJ.Runtime.Infrastructure.DI
{
	public class TestService : ITestService
	{
		public void Test()
		{
			Debug.Log(1);
		}
	}
}