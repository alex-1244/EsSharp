using System;
using System.Text.RegularExpressions;
using EsSharp.UserManagementBoundedContext.Users.Events;

namespace EsSharp.UserManagementBoundedContext.Users
{
	public partial class User: Aggregate
	{
		public string Name { get; private set; }

		public string FamilyName { get; private set; }

		public string Email { get; private set; }

		public bool IsActive { get; private set; }

		public User()
		{
			this.Id = Guid.NewGuid();
		}

		public void RegisterUser(string name, string familyName, string email)
		{
			this.Name = name;
			this.FamilyName = familyName;
			this.Email = email;
			this.PublishEvent(new UserRegistered(this.Id, this.Version, name, familyName, email));
		}

		public void ActivateUser()
		{
			this.IsActive = true;
			this.PublishEvent(new UserActivated(this.Id, this.Version));
		}

		public void SuspendUser(string reason)
		{
			if (string.IsNullOrEmpty(reason))
			{
				throw new AggregateException("Reason is required for user to be suspended");
			}

			this.IsActive = false;
			this.PublishEvent(new UserSuspended(this.Id, this.Version, reason));
		}

		private void ValidateUser(string name, string familyName, string email)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new AggregateException("User name is required");
			}

			if (string.IsNullOrEmpty(familyName))
			{
				throw new AggregateException("User family name is required");
			}

			if (string.IsNullOrEmpty(email))
			{
				throw new AggregateException("User email is required");
			}

			if (!Regex.Match(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase).Success)
			{
				throw new AggregateException("User email is in invalid format");
			}
		}
	}
}
