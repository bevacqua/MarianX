using MarianX.Map.Builder.Levels;
using MarianX.Map.Interface;

namespace MarianX.Map.Builder
{
	public class WorldBuilder : MapBuilder
	{
		public void FullBuild()
		{
			var levels = new IBuilder[] 
			{
				new LevelOne(), new LevelBuilder(2)
			};
			BuildAndSave(levels);
		}
	}
}