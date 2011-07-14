namespace Playground.Web.Models
{
	public class Inhabitant
	{
		private readonly string _name;

		public Inhabitant(string name) {
			_name = name;
		}

		public string Name { get { return _name; } }
	}
}