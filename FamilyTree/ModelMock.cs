namespace FamilyTree
{
	public static class ModelMock
	{
		public static Person Create(int numberOfGenerations)
		{
			return create(numberOfGenerations, 1);
		}

		private static Person create(int currentGeneration, int extend)
		{
			if(currentGeneration == 0)
				return new Person($"Name{extend}", $"FamilyName{extend}", 1900 + extend, 2100 - extend, false, null, null);
			else
			{
				var person1 = create(currentGeneration - 1, extend * 2);
				var person2 = create(currentGeneration - 1, extend * 2 + 1);
				return new Person($"Name{extend}", $"FamilyName{extend}", 1900 + extend, 2100 - extend, false, person1, person2);
			}
		}
	}
}
