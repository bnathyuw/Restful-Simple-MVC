using System.Collections.Generic;

namespace RestfulSimpleMvc.Core
{
	internal class ResponseTypeComparer : IComparer<string> {
		public int Compare(string x, string y) {
			if (x == y) return 0;
			if (x == "text/html") return -1;
			if (y == "text/html") return 1;
			if (x == "application/json") return -1;
			return y == "application/json" ? 1 : 0;
		}
	}
}