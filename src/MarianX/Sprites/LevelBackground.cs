namespace MarianX.Sprites
{
	public class LevelBackground : MapBackground
	{
		private const string IndexFormat = "Content/Map/level_{0}/map.idx";
		private const string SpriteFormat = "Map/level_{0}/map_{1}_{2}";

		public LevelBackground(int level)
			: base(string.Format(IndexFormat, level), SpriteFormat)
		{
		}
	}
}