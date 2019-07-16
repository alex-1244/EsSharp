using System;

namespace EsSharp.UserManagementBoundedContext.Users.Events
{
	[Serializable]
	public class UserActivated : IEvent
	{
		public UserActivated(Guid aggregateId, int expectedVersion)
		{
			this.EventId = Guid.NewGuid();
			this.EventType = UserEventTypes.UserActivated.ToString();
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }
	}
}