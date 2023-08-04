namespace FamilyTree
{
	public class Person
	{
		public static Person NoPerson { get; } = new Person("", "", null, null, false, null, null);
		public Person(string name, string familyName, int? birthYear, int? deathYear, bool isAlive, Person father, Person mother)
		{
			Name = name;
			FamilyName = familyName;
			BirthYear = birthYear;
			DeathYear = deathYear;
			IsAlive = isAlive;
			Father = father;
			Mother = mother;
		}
		public string Name { get; }
		public string FamilyName { get; }
		public int? BirthYear { get; }
		public int? DeathYear { get; }
		public bool IsAlive { get; }
		public Person Father { get; }
		public Person Mother { get; }
	}
}
