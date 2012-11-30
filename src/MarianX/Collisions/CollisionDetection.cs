using MarianX.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class CollisionDetection
	{
		public bool CanMove(AxisAlignedBoundingBox boundingBox, Vector2 interpolation)
		{
			TileMatrix matrix = TileMatrix.Instance;

			//// TODO: get edges as each tile in the AABB, considering direction (from interpolation), and axis.

			//foreach (Tile tile in tiles)
			//{
			//    var x = matrix.GetNextTile(edge, interpolation, Axis.X);
			//    if (x == null || x.Impassable)
			//    {
			//        return false;
			//    }

			//    var y = matrix.GetNextTile(edge, interpolation, Axis.Y);
			//    if (y == null || y.Impassable)
			//    {
			//        return false;
			//    }
			//}

			return true;
		}
	}
}