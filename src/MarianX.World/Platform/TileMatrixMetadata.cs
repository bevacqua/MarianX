using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public class TileMatrixMetadata
	{
		/// <summary>
		/// Columns, then Rows.
		/// </summary>
		public Tile[,] Tiles { get; private set; }

		public TileMatrixMetadata(Rectangle size)
		{
			int rows = size.Height / Tile.Height;
			int cols = size.Width / Tile.Width;

			Tiles = new Tile[cols, rows];

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					Tiles[x, y] = DefaultTileMetadata(x, y);
				}
			}

			Tiles[14, rows - 2].Impassable = true;

			for (int x = 0; x < cols; x++)
				Tiles[x, rows - 1].Impassable = true;
		}

		private Tile DefaultTileMetadata(int x, int y)
		{
			Tile tile = new Tile
			{
				Position = new Point(x * Tile.Width, y * Tile.Height)
			};
			return tile;
		}
	}
}