using MarianX.Enum;
using MarianX.Interface;
using MarianX.Sprites;
using MarianX.World.Configuration;
using MarianX.World.Physics;
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
			FloatRectangle bounds = aabb.Bounds;

			MoveResult result = CollisionDetection.CanMove(bounds, interpolation);

			var moveX = result.HasFlag(MoveResult.X) && !result.HasFlag(MoveResult.BlockedOnX);
			if (moveX)
			{
				aabb.Position.X += interpolation.X;
			}

			bool surfaced = CanSetSurfaced(hitBox);
			var moveY = result.HasFlag(MoveResult.Y) && !result.HasFlag(MoveResult.BlockedOnY);
			if (moveY || !surfaced)
			{
				hitBox.State = HitBoxState.Airborne;
				aabb.Position.Y += interpolation.Y;
			}
			else
			{
				hitBox.State = HitBoxState.Surfaced;
			}

			var reverse = CollisionDetection.CanMove(bounds, -interpolation);
			if (reverse.HasFlag(MoveResult.BlockedOnX) && moveX) // fix issue when moving on X axis to the left.
			{
				aabb.Position.X -= interpolation.X;
			}
			if (reverse.HasFlag(MoveResult.BlockedOnY) && moveY) // fix issue when hitting an impassable on Y axis when jumping.
			{
				aabb.Position.Y -= interpolation.Y; // TODO blocked on Y+ Y-, X+ X-..
			}
			return result;
		}

		private bool CanSetSurfaced(IHitBox hitBox)
		{
			FloatRectangle bounds = hitBox.BoundingBox.Bounds;
			float x = bounds.X;
			float w = bounds.Width;
			float y = bounds.Bottom;
			float h = MagicNumbers.AcceptableSurfaceDistance;

			var rectangle = new FloatRectangle(x, y, w, h);

			var fit = CollisionDetection.CanFitInMatrix(rectangle);
			if (fit == FitResult.Solid) // ensure that the surface actually blocks further movement.
			{
				return true;
			}

			return false;
		}

		public Vector2 Interpolated(GameTime gameTime)
		{
			return InterpolationCalculator.CalculateInterpolation(gameTime);
		}
	}
}