using System;
using System.Collections.Generic;

namespace RestfulSimpleMvc.Core
{
	public class NamesAttribute : Attribute
	{
		private readonly string[] _stringRepresentations;

		public NamesAttribute(params string[] stringRepresentations) {
			_stringRepresentations = stringRepresentations;
		}

		public IEnumerable<string> StringRepresentations {
			get { return _stringRepresentations; }
		}
	}
}