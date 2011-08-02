namespace RestfulSimpleMvc.Core.ResponseType
{
	public interface IAcceptHeaderResponseTypeResolver {
		ResponseType? Resolve(string sourceString);
	}
}