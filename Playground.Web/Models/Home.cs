using System.Runtime.Serialization;

namespace Playground.Web.Models
{
	[DataContract]
	public class Home {
		
		private string _streetAddress;
		private string _locality;

		public Home(string streetAddress, string locality) {
			_streetAddress = streetAddress;
			_locality = locality;
		}

		[DataMember]
		public string StreetAddress
		{
			get { return _streetAddress; }
			internal set { _streetAddress = value; }
		}
		[DataMember]
		public string Locality
		{
			get { return _locality; }
			internal set { _locality = value; }
		}

		public override string ToString() {
			return string.Format("{0}, {1}", _streetAddress, _locality);
		}
	}
}