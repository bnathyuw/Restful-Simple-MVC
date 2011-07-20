namespace RestfulSimpleMvc.Core
{
	public interface IResponseTypeResolver {
		ResponseType? Resolve(string sourceString);
	}
}