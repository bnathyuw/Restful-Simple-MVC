namespace RestfulSimpleMvc.Core.Routes
{
	public enum ResponseType
	{
		[Names("text/html")]
		Html,
		[Names("text/json", "application/json")]
		Json,
		[Names("text/xml", "application/xml")]
		Xml,
		[Names("text/json-p", "application/json-p")]
		JsonP
	}
}