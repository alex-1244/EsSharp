using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EsSharp.ShopBoundedContext.Customers;
using EsSharp.UserManagementBoundedContext.Users.Events;

namespace EsSharp.UserManagementBoundedContext.Users
{
	public partial class User : Aggregate
	{
		public string Name { get; private set; }

		public string FamilyName { get; private set; }

		public string Email { get; private set; }

		public bool IsActive { get; private set; }

		public User(Guid id)
		{
			this.Id = id;
		}

		public User(string name, string familyName, string email) : this(Guid.NewGuid())
		{
			this.Name = name;
			this.FamilyName = familyName;
			this.Email = email;
			this.Id = Guid.NewGuid();
			this.Validate();
			this.PublishEvent(new UserRegistered(this.Id, this.Version, this.Name, this.FamilyName, this.Email));
		}

		public void Activate()
		{
			this.IsActive = true;
			this.PublishEvent(new UserActivated(this.Id, this.Version));
		}

		public void Suspend(string reason)
		{
			if (string.IsNullOrEmpty(reason))
			{
				throw new AggregateException("Reason is required for user to be suspended");
			}

			this.IsActive = false;
			this.PublishEvent(new UserSuspended(this.Id, this.Version, reason));
		}

		private void Validate()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				throw new AggregateException("User name is required");
			}

			if (string.IsNullOrEmpty(this.FamilyName))
			{
				throw new AggregateException("User family name is required");
			}

			if (string.IsNullOrEmpty(this.Email))
			{
				throw new AggregateException("User email is required");
			}

			if (!Regex.Match(this.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase).Success)
			{
				throw new AggregateException("User email is in invalid format");
			}
		}
	}
}
