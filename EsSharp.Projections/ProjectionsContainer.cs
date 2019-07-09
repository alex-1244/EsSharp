using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EsSharp.Projections
{
	public class ProjectionsContainer
	{
		private List<ProjectionBuilder> _projections;

		public ProjectionsContainer()
		{
			this._projections = new List<ProjectionBuilder>()
			{
				new UserProjectionBuilder()
			};
		}

		public IEnumerable<ProjectionBuilder<T>> GetProjectionBuilders<T>() where T: Aggregate
		{
			return this._projections.Where(x =>
				x.GetType().GenericTypeArguments.Length == 1 &&
				x.GetType().GenericTypeArguments[0].IsAssignableFrom(typeof(Aggregate))).Select(x => (ProjectionBuilder<T>)x);
		}
	}
}
