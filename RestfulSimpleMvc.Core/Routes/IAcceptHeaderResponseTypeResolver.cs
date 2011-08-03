namespace RestfulSimpleMvc.Core.Routes
{
	public interface IAcceptHeaderResponseTypeResolver {
		ResponseType? Resolve(string sourceString);
	}
}