using System.Collections.Generic;

namespace MarianX.Sprites
{
	public class CityBackground : ScrollingBackground
	{
		private static readonly IList<string> backgroundAssets = new[]
		{
			"Backgrounds/congress",
			"Backgrounds/eiffel",
			"Backgrounds/island",
			"Backgrounds/louvre",
			"Backgrounds/ny",
			"Backgrounds/opera",
			"Backgrounds/panama"
		};

		public CityBackground()
			: base(backgroundAssets)
		{
		}
	}
}