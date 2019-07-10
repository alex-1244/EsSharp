using System;
using System.Data;
using System.Data.SqlClient;

namespace EsSharp.App.Database
{
	public class ProjectionsDatabaseConnection : IDisposable
	{
		public IDbConnection Connection { get; }

		public ProjectionsDatabaseConnection(string connectionString)
		{
			this.Connection = new SqlConnection(connectionString);
		}

		public void Dispose()
		{
			Connection?.Close();
			Connection?.Dispose();
		}
	}
}
