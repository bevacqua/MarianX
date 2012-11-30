using MarianX.Extensions;
using MarianX.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class CollisionDetection
	{
		public bool CanMove(AxisAlignedBoundingBox boundingBox, Vector2 interpolation)
		{
			TileMatrix matrix = TileMatrix.Instance;

			Rectangle bounds = boundingBox.Bounds;

			bounds.Offset(interpolation);

			return matrix.CanFit(boundingBox, bounds);
		}
	}
}