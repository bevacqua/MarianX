using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public sealed class TileMatrix : TileMatrixMetadata
	{
		private static TileMatrix instance;

		public static TileMatrix Instance
		{
			get { return instance; }
		}

		public static void Initialize(Rectangle bounds)
		{
			instance = new TileMatrix(bounds);
		}

		private TileMatrix(Rectangle size)
			: base(size)
		{
		}

		public IList<Tile> Intersect(Rectangle bounds)
		{
			IList<Tile> intersection = new List<Tile>();

			// calculate possible tile bounds to improve performance.
			int colStart = Math.Max(0, bounds.Left / Tile.Width - 1);
			int colItems = bounds.Width / Tile.Width + 2;
			int colLength = Math.Min(Tiles.GetLength(0), colStart + colItems + 1);

			int rowStart = Math.Max(0, bounds.Top / Tile.Height - 1);
			int rowItems = bounds.Height / Tile.Height + 2;
			int rowLength = Math.Min(Tiles.GetLength(1), rowStart + rowItems + 1);

			for (int x = colStart; x < colLength; x++)
			{
				for (int y = rowStart; y < rowLength; y++)
				{
					Tile tile = Tiles[x, y];

					bool intersects = tile.Bounds.Intersects(bounds);
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