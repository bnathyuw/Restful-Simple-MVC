using System.Collections.Generic;

namespace Playground.Mvc
{
	public interface IEnumNameParser<T> {
		Dictionary<string, T> ParseNames();
	}
}