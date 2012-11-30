using MarianX.Interface;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class Movement
	{
		public bool Move(IHitBox hitBox, Vector2 interpolation)
		{
			CollisionDetection cd = new CollisionDetection();
			AxisAlignedBoundingBox aabb = hitBox.BoundingBox;

			bool canMove = cd.CanMove(aabb, interpolation);
			if (canMove)
			{
				aabb.Position.X += interpolation.X;
				aabb.Position.Y += interpolation.Y; // Y, slopes?
			}

			return canMove;
		}
	}
}