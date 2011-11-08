using System.Web.Mvc;

namespace RestfulSimpleMvc.Core.Location {
	public abstract class LocationProvider<T>:ILocationProvider {
		public string GetLocation(object content, ControllerContext context) {
			return GetLocation((T) content, context);
		}

		protected abstract string GetLocation(T content, ControllerContext context);
	}
}