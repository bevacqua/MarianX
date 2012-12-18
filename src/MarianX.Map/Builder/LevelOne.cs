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
			get { return 200; }
		}

		protected override int Rows
		{
			get { return 45; }
		}

		protected override TileType SelectTileType(TileType[] tileTypes, int x, int y)
		{
			TileType tile = GetBaseTileSet(tileTypes, x, y);
			tile = GetDeeperTileSet(tile, tileTypes, x, y);

			if (x > 98 && x < 101)
			{
				if (y == 22)
				{
					tile = tileTypes[0];
				}
				else if (y == 23)
				{
					tile = tileTypes[5];
				}
				else if (y == 24)
				{
					tile = tileTypes[6];
				}
			}

			if (x == 101 && y == 24)
			{
				tile = tileTypes[9];
			}
			else if (x == 105 && y == 28)
			{
				tile = tileTypes[10];
			}
			else if (x == 106 && y == 28)
			{
				tile = tileTypes[1];
			}
			else if (x == 107 && y == 28)
			{
				tile = tileTypes[9];
			}

			if (x > 98 && x < 103 && y == 32)
			{
				tile = tileTypes[2];
			}

			if (x > 88 && x < 92)
			{
				if (y == 33)
				{
					tile = tileTypes[2];
				}
				else if (y == 34)
				{
					tile = tileTypes[1];
				}
			}

			return tile;
		}

		private TileType GetDeeperTileSet(TileType tile, TileType[] tileTypes, int x, int y)
		{
			if (x > 34 && x < 59)
			{
				if (y == Rows - 22)
				{
					tile = tileTypes[0];
				}
				else if (y == Rows - 21)
				{
					tile = tileTypes[5];
				}
				else if (y == Rows - 20)
				{
					tile = tileTypes[6];
				}
			}

			if (x > 9 && x < 14)
			{
				if (y >= Rows - 22 && y < Rows - 15)
				{
					if (x == 10 && y == Rows - 19)
					{
						tile = tileTypes[9];
					}
					else if (x == 13 && y == Rows - 21)
					{
						tile = tileTypes[10];
					}
					else
					{
						tile = tileTypes[0];
					}
				}
			}

			if (x > 12 && x < 19 && y == Rows - 13)
			{
				tile = tileTypes[4];
			}

			if (x > 23 && x < 26)
			{
				if (y == Rows - 9)
				{
					tile = tileTypes[4];
				}
				else if (y == Rows - 8)
				{
					tile = tileTypes[3];
				}
			}

			if (x > 60 && x < 67)
			{
				if (y >= Rows - 22 && y < Rows - 15)
				{
					if (x == 61 && y == Rows - 21)
					{
						tile = tileTypes[9];
					}
					else if (x == 66 && y == Rows - 19)
					{
						tile = tileTypes[10];
					}
					else
					{
						tile = tileTypes[0];
					}
				}
			}

			if (x > 57 && x < 61 && y == Rows - 13)
			{
				tile = tileTypes[4];
			}

			if (x == 63 && y == Rows - 16)
			{
				tile = tileTypes[4];
			}

			return tile;
		}

		private TileType GetBaseTileSet(TileType[] tileTypes, int x, int y)
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

			if (y >= Rows - 21 && y < Rows - 17)
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
				if (y == Rows - 7)
				{
					if (x > 60 && x < 110)
					{
						if (x > 75 && x < 85)
						{
							tile = tileTypes[5];
						}
						else if (x > 75)
						{
							tile = tileTypes[13];
						}
						else
						{
							tile = tileTypes[11];
						}
					}
					else
					{
						tile = tileTypes[3];
					}
				}
				else if (y == Rows - 8)
				{
					if ((x > 42 && x < 48) || x > 75)
					{
						tile = tileTypes[0];
					}
					else if (x > 60 && x < 110)
					{
						tile = tileTypes[13];
					}
					else
					{
						tile = tileTypes[4];
					}
				}
				else if (y == Rows - 6 && x > 60 && x < 110)
				{
					if (x > 75 && x < 85)
					{
						tile = tileTypes[12];
					}
					else
					{
						tile = tileTypes[3];
					}
				}
			}

			if (x > 28 && x < 34 && y == 18)
			{
				tile = tileTypes[4];
			}

			if (x > 44 && x < 54)
			{
				if (y == 16)
				{
					tile = tileTypes[3];
				}
				else if (y == 15)
				{
					tile = tileTypes[4];
				}
			}

			return tile;
		}
	}
}