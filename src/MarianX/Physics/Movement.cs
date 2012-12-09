using MarianX.Enum;
using MarianX.Interface;
using MarianX.Sprites;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class Movement
	{
		public CollisionDetection CollisionDetection { get; private set; }
		public InterpolationCalculator InterpolationCalculator { get; private set; }

		public Movement(CollisionDetection collisionDetection, InterpolationCalculator interpolationCalculator)
		{
			CollisionDetection = collisionDetection;
			InterpolationCalculator = interpolationCalculator;
		}

		public MoveResult Move(IHitBox hitBox, Vector2 interpolation)
		{
			AxisAlignedBoundingBox aabb = hitBox.BoundingBox;
			MoveResult result = CollisionDetection.CanMove(aabb.Bounds, interpolation);

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

			if (interpolation.X < 0) // fix issue when moving on X axis to the left.
			{
				var reverse = CollisionDetection.CanMove(aabb.Bounds, -interpolation);
				if (reverse == MoveResult.Blocked)
				{
					aabb.Position.X -= interpolation.X;
				}
			}
			return result;
		}

		public Vector2 Interpolated(GameTime gameTime)
		{
			return InterpolationCalculator.CalculateInterpolation(gameTime);
		}
	}
}