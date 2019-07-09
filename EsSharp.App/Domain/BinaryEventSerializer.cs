using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EsSharp.App.Domain
{
	public class BinaryEventSerializer : IEventSerializer
	{
		private readonly BinaryFormatter _formatter;

		public BinaryEventSerializer()
		{
			this._formatter = new BinaryFormatter();
		}

		public byte[] Serialize(IEvent ev)
		{
			using (var ms = new MemoryStream())
			{
				this._formatter.Serialize(ms, ev);
				return ms.ToArray();
			}
		}

		public T Deserialize<T>(byte[] evData) where T : IEvent
		{
			using (var memStream = new MemoryStream())
			{
				memStream.Write(evData, 0, evData.Length);
				memStream.Seek(0, SeekOrigin.Begin);
				var obj = (T)this._formatter.Deserialize(memStream);
				return obj;
			}
		}
	}
}
