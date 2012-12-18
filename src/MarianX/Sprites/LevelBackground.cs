using MarianX.World.Extensions;

namespace MarianX.Sprites
{
	public class LevelBackground : MapBackground, ILevel
	{
		private const string IndexFormat = "Content/Map/level_{0}/map.idx";
		private const string SpriteFormat = "Map/level_{0}/map_{1}_{2}";

		public bool Active { get; set; }

		public int Level { get; private set; }

		public LevelBackground(int level)
			: base(string.Format(IndexFormat, level), SpriteFormat)
		{
			Level = level;
		}
	}
}