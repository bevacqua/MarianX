using System.Collections.Generic;
using MarianX.Extensions;
using MarianX.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class CollisionDetection
	{
		public bool CanMove(AxisAlignedBoundingBox boundingBox, Vector2 interpolation)
		{
			Rectangle bounds = boundingBox.Bounds;
			Rectangle to = bounds.Offset(interpolation);

			return CanFitInMatrix(to);
		}

		private bool CanFitInMatrix(Rectangle bounds)
		{
			TileMatrix matrix = TileMatrix.Instance;
			IList<Tile> intersection = matrix.Intersect(bounds);

			foreach (Tile tile in intersection)
			{
				if (tile.Impassable)
				{
					return false;
				}
			}

			return true;
		}
	}
}