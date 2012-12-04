using System;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.Sprites;
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

		public MoveResult Move(IHitBox hitBox, Vector2 interpolation)
		{
			AxisAlignedBoundingBox aabb = hitBox.BoundingBox;

			MoveResult result = collisionDetection.CanMove(aabb.Bounds, interpolation);

			if (result.HasFlag(MoveResult.X))
			{
				aabb.Position.X += interpolation.X;
			}

			if (result.HasFlag(MoveResult.Y))
			{
				hitBox.State = HitBoxState.Airborne;
				aabb.Position.Y += interpolation.Y;
			}
			else
			{
				hitBox.State = HitBoxState.Surfaced;
			}

			return result;
		}
	}
}