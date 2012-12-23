using System.Collections.Generic;
using MarianX.Map.Builder.Levels;
using MarianX.Map.Interface;

namespace MarianX.Map.Builder
{
	public class WorldBuilder
	{
		public void FullBuild()
		{
			var mapBuilder = new MapBuilder();
			var levels = new List<IBuilder>
			{
				new LevelOne()
			};
			for (int i = 2; i <= 3; i++)
			{
				var level = new LevelBuilder(i);
				levels.Add(level);
			}
			mapBuilder.BuildAndSave(levels);
		}
	}
}