using System.IO;

namespace Playground.Mvc.Serializers
{
	public interface ISerializer
	{
		void WriteXmlToStream(object content, Stream stream);
		void WriteJsonToStream(object content, Stream stream);
	}

	public interface ISerializer<in T> : ISerializer
	{
		void WriteJsonToStream(T content, Stream stream);
		void WriteXmlToStream(T content, Stream stream);
	}
}