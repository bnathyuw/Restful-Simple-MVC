namespace RestfulSimpleMvc.Core.ResponseType
{
	public interface IResponseTypeResolver {
		ResponseType? Resolve(string sourceString);
	}
}