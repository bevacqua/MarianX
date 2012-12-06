using MarianX.Enum;
using MarianX.Interface;
using MarianX.Sprites;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class Movement
	{
		private readonly CollisionDetection collisionDetection;
		private readonly InterpolationCalculator interpolationCalculator;

		public Movement(CollisionDetection collisionDetection, InterpolationCalculator interpolationCalculator)
		{
			this.collisionDetection = collisionDetection;
			this.interpolationCalculator = interpolationCalculator;
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

		public Vector2 Interpolated(GameTime gameTime)
		{
			return interpolationCalculator.CalculateInterpolation(gameTime);
		}
	}
}