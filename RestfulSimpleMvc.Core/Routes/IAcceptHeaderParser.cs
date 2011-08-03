using System.Collections.Generic;
using System.Linq;

namespace RestfulSimpleMvc.Core.Routes
{
	public interface IAcceptHeaderParser {
		IEnumerable<IGrouping<decimal, string>> GetAcceptedTypes(string accept);
	}
}