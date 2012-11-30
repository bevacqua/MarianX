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

			bounds.Offset(interpolation);

			return CanFitInMatrix(bounds);
		}

		private bool CanFitInMatrix(Rectangle bounds)
		{
			TileMatrix matrix = TileMatrix.Instance;
			Tile[] intersection = matrix.Intersect(bounds);

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