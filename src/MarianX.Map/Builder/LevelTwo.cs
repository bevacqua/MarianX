using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Map.Builder
{
	public class LevelTwo : TileMatrixBuilder
	{
		public override int Level
		{
			get { return 2; }
		}

		public override Vector2 StartPosition
		{
			get { return new Vector2(85, 160); }
		}

		protected override int Columns
		{
			get { return 125; }
		}

		protected override int Rows
		{
			get { return 24; }
		}

		protected override TileType SelectTileType(TileType[] tileTypes, int x, int y)
		{
			int index = 0;

			if (y == Rows - 3)
			{
				index = 4;
			}
			else if (y >= Rows - 2)
			{
				index = 3;
			}

			return tileTypes[index];
		}
	}
}