using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EsSharp
{
	public abstract class ProjectionBuilder
	{
	}

	public class ProjectionBuilder<T> : ProjectionBuilder where T: Aggregate
	{
		private readonly Dictionary<Type, MethodInfo> _eventsToHandle;
		private readonly bool _throwOnUnsupportedEvent;

		protected ProjectionBuilder(string handlerMethodsName = "Handle", bool throwOnUnsupportedEvent = false)
		{
			var methods = this.GetType()
				.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic)
				.Where(x=>x.Name == handlerMethodsName)
				.Where(x => x.GetParameters().Length == 1 &&
				            x.GetParameters()[0].ParameterType.GetInterfaces().Any(i => i == typeof(IEvent)));

			this._eventsToHandle = methods.ToDictionary(x=>x.GetParameters()[0].ParameterType, x=>x);
			this._throwOnUnsupportedEvent = throwOnUnsupportedEvent;
		}

		public void HandleEvents(T aggregate)
		{
			foreach (var @event in aggregate.Events)
			{
				if (this._eventsToHandle.ContainsKey(@event.GetType()))
				{
					this._eventsToHandle[@event.GetType()].Invoke(this, new object[] { @event });
				}
				else if (this._throwOnUnsupportedEvent)
				{
					throw new ArgumentException($"event {@event.GetType()} is unsupported by {this.GetType()} projection builder");
				}
			}
		}
	}
}
