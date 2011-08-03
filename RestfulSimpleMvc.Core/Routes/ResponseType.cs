namespace RestfulSimpleMvc.Core.Routes
{
	public enum ResponseType
	{
		[Names("text/html")]
		Html,
		[Names("application/json")]
		Json,
		[Names("text/xml","application/xml")]
		Xml
	}
}