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
		
		public class Binding<TService> where TService : IService
		{
			private readonly Container _container;

			public Type ServiceType => typeof(TService); 

			public Binding(Container container) => 
				_container = container;

			public DependencyInfo<T> To<T>() where T : class, IService
			{
				Debug.Log($"Service {ServiceType.Name} is registered as instance of {typeof(T).Name}");
				return new DependencyInfo<T>(_container, this);
			}
			
			public class DependencyInfo<T> where T : class, IService
			{
				private Container _container;
				private Binding<TService> _binding;

				public DependencyInfo(Container container, Binding<TService> binding)
				{
					_binding = binding;
					_container = container;
				}

				public void FromMethod(Func<T> constructionMethod) => 
					_container[_binding.ServiceType] = constructionMethod?.Invoke();
			}
		}
	}
}