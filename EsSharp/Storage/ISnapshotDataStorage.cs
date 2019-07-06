using System;

namespace EsSharp.Storage
{
	public interface ISnapshotDataStorage
	{
		AggregateSnapshot Get(Guid aggregateId);
		void Add(AggregateSnapshot snapshot);
		void Commit();
	}
}