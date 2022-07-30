using System.Collections;
using MGJ.Runtime.Infrastructure.DI;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.Services.Coroutines
{
	public interface ICoroutineRunner : IService
	{
		Coroutine StartCoroutine(IEnumerator routine);
		void StopCoroutine(Coroutine routine);
	}
}