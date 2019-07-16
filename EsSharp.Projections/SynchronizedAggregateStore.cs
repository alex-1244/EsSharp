using System;
using System.Transactions;

namespace EsSharp.Projections
{
	public class SynchronizedAggregateStore
	{
		private readonly ProjectionsContainer _projections;
		private readonly EventStore _eventStore;

		public SynchronizedAggregateStore(ProjectionsContainer projections, EventStore eventStore)
		{
			_projections = projections;
			_eventStore = eventStore;
		}

		public void SaveAggregateAndEvents<T>(T aggregate) where T : Aggregate
		{
			using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }))
			{
				foreach (var projection in this._projections.GetProjectionBuilders<T>())
				{
					projection.HandleEvents(aggregate);
				}

				this._eventStore.Add(aggregate);
				this._eventStore.Commit();

				transaction.Complete();
			}
		}

		public T Get<T>(Guid id) where T : Aggregate
		{
			var events = _eventStore.GetEventsForAggregate(id);

			var argTypes = new[] { typeof(Guid) };
			var constructor = typeof(T).GetConstructor(argTypes);
			var argValues = new object[] { id };
			var aggregate = (Aggregate)constructor.Invoke(argValues);

			foreach (var @event in events)
			{
				aggregate.Handle(@event);
			}

			return (T)aggregate;
		}
	}
}
