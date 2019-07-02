using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EsSharp.Tests.TestEntities.Events;

namespace EsSharp.Tests.TestEntities.ProjectionBuilders
{
	public class CarProjectionBuilder
	{
		private HashSet<Type> EventsToHandle { get; }

		public IEvent LastEvent { get; private set; }

		public CarProjectionBuilder()
		{
			var methods = this.GetType()
				.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic).AsEnumerable()
				.Where(x => x.GetParameters().Length == 1 &&
				            x.GetParameters()[0].ParameterType.GetInterfaces().Any(i => i == typeof(IEvent)))
				.Select(x => x.GetParameters()[0].ParameterType);

			this.EventsToHandle = new HashSet<Type>(methods);
		}

		public void Handle(Aggregate aggregate)
		{
			foreach (var domainEvent in aggregate.Events)
			{
				if (this.EventsToHandle.Contains(domainEvent.GetType()))
				{
					this.Handle((dynamic)domainEvent);
				}
			}
		}

		private void Handle(TyreChangedEvent domainEvent)
		{
			this.LastEvent = domainEvent;
		}
	}
}