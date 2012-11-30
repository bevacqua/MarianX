using Microsoft.Xna.Framework;

namespace MarianX.Platform
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
		}

		private Tile DefaultTileMetadata(int x, int y)
		{
			Tile tile = new Tile
			{
				Position = new Point(x, y)
			};
			return tile;
		}
	}
}