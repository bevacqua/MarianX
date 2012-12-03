using System;
using MarianX.Interface;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class Movement
	{
		private readonly CollisionDetection collisionDetection;

		public Movement(CollisionDetection collisionDetection)
		{
			if (collisionDetection == null)
			{
				throw new ArgumentNullException("collisionDetection");
			}
			this.collisionDetection = collisionDetection;
		}

		public bool Move(IHitBox hitBox, Vector2 interpolation)
		{
			AxisAlignedBoundingBox aabb = hitBox.BoundingBox;

			bool canMove = collisionDetection.CanMove(aabb, interpolation);
			if (canMove)
			{
				aabb.Position.X += interpolation.X;
				aabb.Position.Y += interpolation.Y; // TODO: Y, slopes?
			}

			return canMove;
		}
	}
}