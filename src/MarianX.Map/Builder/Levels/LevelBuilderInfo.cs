using System;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Map.Builder.Levels
{
	public class LevelBuilderInfo
	{
		private readonly Lazy<Vector2> start;

		public int Level { get; set; }
		public int Columns { get; set; }
		public int Rows { get; set; }
		public int StartX { get; set; }
		public int StartY { get; set; }

		public int ScreenTop { get; set; }
		public int ScreenLeft { get; set; }
		public int ScreenBottom { get; set; }
		public int ScreenRight { get; set; }

		public Vector2 Start
		{
			get { return start.Value; }
		}

		public LevelBuilderInfo()
		{
			start = new Lazy<Vector2>(() => new Vector2(StartX * Tile.Width, StartY * Tile.Height));
		}
	}
}