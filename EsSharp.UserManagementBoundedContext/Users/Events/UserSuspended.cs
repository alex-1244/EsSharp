using System;

namespace EsSharp.UserManagementBoundedContext.Users.Events
{
	[Serializable]
	public class UserSuspended : IEvent
	{
		public UserSuspended(Guid aggregateId, int expectedVersion, string reason)
		{
			this.EventId = Guid.NewGuid();
			this.EventType = UserEventTypes.UserSuspended.ToString();
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;

			this.Reason = reason;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public string Reason { get; }
	}
}