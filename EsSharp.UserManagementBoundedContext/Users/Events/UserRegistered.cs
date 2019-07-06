using System;

namespace EsSharp.UserManagementBoundedContext.Users.Events
{
	public class UserRegistered: IEvent
	{
		public UserRegistered(
			Guid aggregateId, 
			int expectedVersion, 
			string name, 
			string familyName, 
			string email)
		{
			this.EventId = Guid.NewGuid();
			this.EventType = UserEventTypes.UserRegistered.ToString();
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;

			this.Name = name;
			this.FamilyName = familyName;
			this.Email = email;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public string Name { get; }
		public string FamilyName { get; }
		public string Email { get; }
	}
}
