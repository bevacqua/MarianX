using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MarianX.Platform
{
	public sealed class TileMatrix
	{
		private static readonly TileMatrix instance;

		public static TileMatrix Instance
		{
			get { return instance; }
		}

		static TileMatrix()
		{
			instance = new TileMatrix();
		}

		private TileMatrix()
		{
		}

		/// <summary>
		/// Rows, then Columns.
		/// </summary>
		public Tile[][] Tiles { get; private set; }

		public IList<Tile> Intersect(Rectangle bounds)
		{
			IList<Tile> intersection = new List<Tile>();

			// calculate possible tile bounds to improve performance.
			int rowStart = bounds.Top / Tile.Height - 1;
			int rowItems = bounds.Height / Tile.Height + 2;
			
			int colStart = bounds.Left / Tile.Width - 1;
			int colItems = bounds.Width / Tile.Width + 2;

			foreach (Tile[] row in Tiles.Skip(rowStart).Take(rowItems))
			{
				foreach (Tile tile in row.Skip(colStart).Take(colItems))
				{
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