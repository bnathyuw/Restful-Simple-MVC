using System.Collections.Generic;

namespace Playground.Mvc
{
	internal class ResponseTypeComparer : IComparer<string> {
		public int Compare(string x, string y) {
			if (x == y) return 0;
			if (x == "text/html") return -1;
			if (y == "text/html") return 1;
			if (x == "application/json") return -1;
			if (y == "application/json") return 1;
			return 0;
		}
	}
}