using System.Collections.Generic;

namespace RestfulSimpleMvc.Core
{
	public interface IEnumNameParser<T> {
		Dictionary<string, T> ParseNames();
	}
}