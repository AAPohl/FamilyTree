using System.Xml.Linq;

namespace FamilyTree
{
	public static class PersonDeserializer
	{
		public static Person Deserialize(XElement root)
		{
			if (root == null)
				return null;

			var name = root.Attribute("Name").Value;
			var familiyName = root.Attribute("FamilyName").Value;
			int.TryParse(root.Attribute("Birth")?.Value, out var birthYear);
			int.TryParse(root.Attribute("Death")?.Value, out var deathYear);
			bool.TryParse(root.Attribute("IsAlive")?.Value, out var isAlive);
			var father = Deserialize(root.Element("Father"));
			var mother = Deserialize(root.Element("Mother"));
			return new Person(name, familiyName, birthYear, deathYear, isAlive, father, mother);
		}
	}
}
