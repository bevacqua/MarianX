using System;
using System.Collections.Generic;
using MarianX.World.Physics;

namespace MarianX.World.Platform
{
	public sealed class TileMatrix : TileMatrixMetadata
	{
		private static TileMatrix instance;

		public static TileMatrix Instance
		{
			get { return instance; }
		}

		public static void Use(string path)
		{
			instance = new TileMatrix(path);
		}

		private TileMatrix(string path)
			: base(path)
		{
		}

		public IList<Tile> Intersect(FloatRectangle bounds)
		{
			IList<Tile> intersection = new List<Tile>();

			// calculate possible tile bounds to improve performance.
			int colStart = Math.Max(0, (int)bounds.Left / Tile.Width - 1);
			int colItems = (int)bounds.Width / Tile.Width + 2;
			int colLength = Math.Min(Tiles.GetLength(0), colStart + colItems + 1);

			int rowStart = Math.Max(0, (int)bounds.Top / Tile.Height - 1);
			int rowItems = (int)bounds.Height / Tile.Height + 2;
			int rowLength = Math.Min(Tiles.GetLength(1), rowStart + rowItems + 1);

			for (int x = colStart; x < colLength; x++)
			{
				for (int y = rowStart; y < rowLength; y++)
				{
					Tile tile = Tiles[x, y];

					var intersects = bounds.Intersects(tile.Bounds);
					if (intersects)
					{
						intersection.Add(tile);
					}
				}
			}

			return intersection;
		}
	}
}