namespace Playground.Web.Mvc
{
	public interface IResponseTypeResolver {
		ResponseType? Resolve(string sourceString);
	}
}