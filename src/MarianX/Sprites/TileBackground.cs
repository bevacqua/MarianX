using System.Collections.Generic;

namespace MarianX.Sprites
{
	public class TileBackground : ScrollingBackground
	{
		private static readonly IList<string> backgroundAssets = new[]
		{
			"Backgrounds/tilematrix"
		};

		public TileBackground()
			: base(backgroundAssets)
		{
		}
	}
}