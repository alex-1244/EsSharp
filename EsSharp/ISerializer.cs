namespace EsSharp
{
	public interface IEventSerializer
	{
		byte[] Serialize(IEvent ev);

		T Deserialize<T>(byte[] evData) where T : IEvent;
	}

	public interface IAggregateSerializer
	{
		byte[] Serialize(Aggregate agg);

		T Deserialize<T>(byte[] aggData) where T : Aggregate;
	}
}
