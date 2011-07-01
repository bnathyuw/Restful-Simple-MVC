namespace Playground.Web.Models
{
	public class Home {
		
		private readonly string _streetAddress;
		private readonly string _locality;

		public Home(string streetAddress, string locality) {
			_streetAddress = streetAddress;
			_locality = locality;
		}

		public string StreetAddress { get { return _streetAddress; } }
		public string Locality { get { return _locality; } }

		public override string ToString() {
			return string.Format("{0}, {1}", _streetAddress, _locality);
		}
	}
}