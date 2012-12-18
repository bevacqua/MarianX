using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Map.Builder
{
	public class LevelOne : TileMatrixBuilder
	{
		public override int Level
		{
			get { return 1; }
		}

		public override Vector2 StartPosition
		{
			get { return new Vector2(85, 450); }
		}

		protected override int Columns
		{
			get { return 198; }
		}

		protected override int Rows
		{
			get { return 45; }
		}

		protected override TileType SelectTileType(TileType[] tileTypes, int x, int y)
		{
			TileType tile = tileTypes[0];

			if (y == Rows - 2)
			{
				return tileTypes[7];
			}
			else if (y > Rows - 2)
			{
				return tileTypes[8];
			}

			if (x > 100 && x < 106)
			{
				return tile;
			}

			if (y == Rows - 22)
			{
				if (x > 20 && x < 25)
				{
					tile = tileTypes[0];
				}
				else
				{
					tile = tileTypes[2];
				}
			}

			if (y >= Rows - 21 && y < Rows - 9)
			{
				if (x > 20 && x < 25 && y == Rows - 21)
				{
					tile = tileTypes[5];
				}
				else if (x > 20 && x < 25 && y == Rows - 20)
				{
					tile = tileTypes[6];
				}
				else
				{
					tile = tileTypes[1];
				}
			}
			else if ((x > 35 && x < 55) || (x > 60 && x < 110))
			{
				if (y == Rows - 3)
				{
					tile = tileTypes[3];
				}
				else if (y == Rows - 4)
				{
					tile = tileTypes[4];
				}
			}

			if (x > 28 && x < 36)
			{
				if (y == 19)
				{
					tile = tileTypes[3];
				}
				else if (y == 18)
				{
					tile = tileTypes[4];
				}
			}

			return tile;
		}
	}
}