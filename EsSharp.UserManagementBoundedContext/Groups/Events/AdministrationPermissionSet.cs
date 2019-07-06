using System;

namespace EsSharp.UserManagementBoundedContext.Groups.Events
{
	public class AdministrationPermissionSet : IEvent
	{
		public AdministrationPermissionSet(Guid aggregateId, int expectedVersion, bool permissionValue)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;
			this.EventId = Guid.NewGuid();
			this.EventType = GroupEventTypes.AdministrationPermissionSet.ToString();

			this.PermissionValue = permissionValue;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public bool PermissionValue { get; }
	}
}
