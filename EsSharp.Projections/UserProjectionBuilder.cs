using Dapper;
using EsSharp.App.Database;
using EsSharp.UserManagementBoundedContext.Users;
using EsSharp.UserManagementBoundedContext.Users.Events;

namespace EsSharp.Projections
{
	public class UserProjectionBuilder : ProjectionBuilder<User>
	{
		private readonly ProjectionsDatabaseConnection _database;

		public UserProjectionBuilder(ProjectionsDatabaseConnection database) : base()
		{
			this._database = database;
			this._database.Connection.Open();
		}

		public void Handle(UserRegistered @event)
		{
			this._database.Connection.Execute(
				$"INSERT INTO Users(Id, Name, FamilyName, Email) values ('{@event.AggregateId}','{@event.Name}','{@event.FamilyName}','{@event.Email}')");
		}

		public void Handle(UserActivated @event)
		{

		}

		public void Handle(UserSuspended @event)
		{

		}
	}
}
