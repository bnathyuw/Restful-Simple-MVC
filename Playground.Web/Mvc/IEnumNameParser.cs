using System.Collections.Generic;

namespace Playground.Web.Mvc
{
	public interface IEnumNameParser<T> {
		Dictionary<string, T> ParseNames();
	}
}