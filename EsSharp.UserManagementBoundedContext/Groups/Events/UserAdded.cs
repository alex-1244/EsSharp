using System;
using EsSharp.UserManagementBoundedContext.Users;

namespace EsSharp.UserManagementBoundedContext.Groups.Events
{
	public class UserAdded : IEvent
	{
		public UserAdded(Guid aggregateId, int expectedVersion, User user)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;
			this.EventId = Guid.NewGuid();
			this.EventType = GroupEventTypes.UserAdded.ToString();

			this.User = user;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public User User { get; }
	}
}