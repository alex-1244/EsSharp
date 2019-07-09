using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.UserManagementBoundedContext.Groups.Events;
using EsSharp.UserManagementBoundedContext.Users;

namespace EsSharp.UserManagementBoundedContext.Groups
{
	public partial class Group : Aggregate
	{
		public string Name { get; private set; }

		private IList<User> _users;
		public IEnumerable<User> Users => _users.AsEnumerable();

		public bool Administration { get; private set; }

		public Group(Guid id)
		{
			this.Id = id;
		}

		public Group(string name) : this(Guid.NewGuid())
		{
			this.Name = name;
			this.PublishEvent(new GroupCreated(this.Id, this.Version, this.Name));
		}

		public void SetAdministrationPermissions()
		{
			this.Administration = true;
			this.PublishEvent(new AdministrationPermissionSet(this.Id, this.Version, true));
		}

		public void ResetAdministrationPermissions()
		{
			this.Administration = false;
			this.PublishEvent(new AdministrationPermissionSet(this.Id, this.Version, false));
		}

		public void AddUser(User user)
		{
			this._users.Add(user);
			this.PublishEvent(new UserAdded(this.Id, this.Version, user));
		}

		public void SuspendUsers(string reason)
		{
			foreach (var user in this._users)
			{
				user.SuspendUser(reason);
			}
		}
	}
}
