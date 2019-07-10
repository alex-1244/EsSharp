using System.Collections.Generic;
using System.Linq;
using EsSharp.App.Database;

namespace EsSharp.Projections
{
	public class ProjectionsContainer
	{
		private readonly List<ProjectionBuilder> _projections;

		public ProjectionsContainer(ProjectionsDatabaseConnection connection)
		{
			this._projections = new List<ProjectionBuilder>()
			{
				new UserProjectionBuilder(connection)
			};
		}

		public IEnumerable<ProjectionBuilder<T>> GetProjectionBuilders<T>() where T: Aggregate
		{
			return this._projections.Where(x =>
				x.GetType().BaseType.GenericTypeArguments.Length == 1 &&
				x.GetType().BaseType.GenericTypeArguments[0].IsAssignableFrom(typeof(T)) &&
				x.GetType().BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(ProjectionBuilder<>))).Select(x => (ProjectionBuilder<T>)x);
		}
	}
}
