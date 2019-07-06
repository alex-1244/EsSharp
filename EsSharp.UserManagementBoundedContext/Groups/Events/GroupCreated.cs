using System;

namespace EsSharp.UserManagementBoundedContext.Groups.Events
{
	public class GroupCreated : IEvent
	{
		public GroupCreated(Guid aggregateId, int expectedVersion, string name)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;
			this.EventId = Guid.NewGuid();
			this.EventType = GroupEventTypes.GroupCreated.ToString();

			this.Name = name;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public string Name { get; }
	}
}