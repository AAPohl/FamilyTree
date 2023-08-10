using System.Collections.Generic;
using System.Drawing;

namespace FamilyTree.Configuration
{	
	public interface IConfiguration
	{
		int GetNumberOfGenerations();
		IEnumerable<float> GetCreateRadii();
		IEnumerable<TextInfo> GetTextInfo(Person person, int currentGeneration);

	}
}
