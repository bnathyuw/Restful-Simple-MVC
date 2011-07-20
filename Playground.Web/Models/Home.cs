using System.Collections.Generic;

namespace RestfulSimpleMvc.Web.Models
{
	public class Home
	{

		private readonly string _streetAddress;
		private readonly string _locality;
		private readonly List<Inhabitant> _inhabitants;

		public Home(string streetAddress, string locality, params Inhabitant[] inhabitants)
		{
			_streetAddress = streetAddress;
			_locality = locality;
			_inhabitants = new List<Inhabitant>(inhabitants);
		}

		public string StreetAddress
		{
			get { return _streetAddress; }
		}

		public string Locality
		{
			get { return _locality; }
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", _streetAddress, _locality);
		}

		public IEnumerable<Inhabitant> Inhabitants { get { return _inhabitants; } }
	}
}