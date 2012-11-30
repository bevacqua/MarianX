using System;
using System.Collections.Generic;
using System.Linq;
using MarianX.Collisions;
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

		public Tile[][] Tiles { get; private set; }

		public Tile[] GetTiles(AxisAlignedBoundingBox boundingBox)
		{
			IList<Tile> tiles = new List<Tile>();

			int x = (int)boundingBox.Position.X / Tile.Width;
			int y = (int)boundingBox.Position.Y / Tile.Height;

			for (int xo = 0; xo < AxisAlignedBoundingBox.TilesWide; xo++)
			{
				for (int yo = 0; yo < AxisAlignedBoundingBox.TilesHigh; yo++)
				{
					Tile tile = Tiles[x + xo][y + yo];
					tiles.Add(tile);
				}
			}

			Tile[] position = tiles.ToArray();
			return position;
		}

		private Tile GetTile(Vector2 position)
		{
			int x = (int)position.X / Tile.Width;
			int y = (int)position.Y / Tile.Height;
			Tile tile = Tiles[x][y];
			return tile;
		}

		public Tile GetNextTile(Vector2 edge, Vector2 interpolation, Axis axis)
		{
			if (axis == Axis.X)
			{
				int x = (int)(edge.X + interpolation.X) / Tile.Width;
				return GetTile(new Vector2(x, edge.Y));
			}
			else if (axis == Axis.Y)
			{
				int y = (int)(edge.Y + interpolation.Y) / Tile.Height;
				return GetTile(new Vector2(edge.X, y));
			}
			else
			{
				throw new NotSupportedException("axis not supported.");
			}
		}

	}
}