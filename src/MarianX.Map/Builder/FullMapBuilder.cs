using MarianX.Map.Builder.Levels;

namespace MarianX.Map.Builder
{
	public class FullMapBuilder : MapBuilder
	{
		public void FullBuild()
		{
			var levels = new TileMatrixBuilder[] 
			{
				new LevelOne(), new LevelTwo()
			};
			BuildAndSave(levels);
		}
	}
}