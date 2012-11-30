using System;
using System.Collections.Generic;
using MarianX.Core;
using Microsoft.Xna.Framework;

namespace MarianX.Platform
{
	public sealed class TileMatrix : TileMatrixMetadata
	{
		private static readonly TileMatrix instance;

		public static TileMatrix Instance
		{
			get { return instance; }
		}

		static TileMatrix()
		{
			Game game = GameCore.Instance;
			instance = new TileMatrix(game.Window.ClientBounds);
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
			int colLength = Math.Min(Tiles.GetLength(0), colStart + colItems);

			int rowStart = Math.Max(0, bounds.Top / Tile.Height - 1);
			int rowItems = bounds.Height / Tile.Height + 2;
			int rowLength = Math.Min(Tiles.GetLength(1), rowStart + rowItems);

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