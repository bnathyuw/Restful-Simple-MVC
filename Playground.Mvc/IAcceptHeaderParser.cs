using System.Collections.Generic;
using System.Linq;

namespace Playground.Mvc
{
	public interface IAcceptHeaderParser {
		IEnumerable<IGrouping<decimal, string>> GetAcceptedTypes(string accept);
	}
}