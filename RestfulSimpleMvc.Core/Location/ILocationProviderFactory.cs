namespace RestfulSimpleMvc.Core.Location {
	public interface ILocationProviderFactory {
		ILocationProvider Build(object content);
	}
}