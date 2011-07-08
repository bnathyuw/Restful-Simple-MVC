namespace Playground.Mvc
{
	public interface IResponseTypeResolver {
		ResponseType? Resolve(string sourceString);
	}
}