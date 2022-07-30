using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.DI
{
	public class Container
	{
		private static Container _instance;

		public static Container Services => _instance ?? (_instance = new Container());
		
		private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

		public Binding<TService> Bind<TService>() where TService : IService =>
			new Binding<TService>(this);

		public TService Resolve<TService>() where TService : IService
			=> (TService)_services[typeof(TService)];

		public void Unbind<TService>() where TService : IService =>
			_services.Remove(typeof(TService));

		private IService this[Type type]
		{
			set => _services[type] = value;
		}
		
		public class Binding<TService>
		{
			private readonly Container _container;

			private Type ServiceType => typeof(TService); 

			public Binding(Container container) => 
				_container = container;

			public void To<T>(Func<T> constructionMethod) where T : class, IService
			{
				_container[ServiceType] = constructionMethod.Invoke();
				Debug.Log($"Service {ServiceType.Name} is registered as instance of {typeof(T).Name}");
			}
		}
	}
}