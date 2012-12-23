using MarianX.Diagnostics;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.World.Configuration;
using MarianX.World.Physics;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class Movement
	{
		public StaticCollisionDetection CollisionDetection { get; private set; }
		public InterpolationCalculator InterpolationCalculator { get; private set; }

		public Movement(StaticCollisionDetection collisionDetection, InterpolationCalculator interpolationCalculator)
		{
			CollisionDetection = collisionDetection;
			InterpolationCalculator = interpolationCalculator;
		}

		public MoveResult Move(IHitBox hitBox, Vector2 interpolation)
		{
			AxisAlignedBoundingBox aabb = hitBox.BoundingBox;

			var result = CollisionDetection.CanMove(aabb.Bounds, interpolation, DetectionType.Collision);
			if (result.HasFlag(MoveResult.LevelCompleted))
			{
				result = MoveResult.LevelCompleted;
			}
			else
			{
				result = Reposition(hitBox, result, interpolation);
			}

			Diagnostic.Write("aabb", aabb.Position);

			return result;
		}

		private MoveResult Reposition(IHitBox hitBox, MoveResult result, Vector2 interpolation)
		{
			AxisAlignedBoundingBox aabb = hitBox.BoundingBox;

			var moveX = result.HasFlag(MoveResult.X)
				&& !result.HasFlag(MoveResult.BlockedOnNegativeX)
				&& !result.HasFlag(MoveResult.BlockedOnPositiveX);

			if (moveX)
			{
				aabb.Position.X += interpolation.X;
			}

			bool surfaced = CanSetSurfaced(hitBox);
			var moveY = result.HasFlag(MoveResult.Y)
				&& !result.HasFlag(MoveResult.BlockedOnNegativeY)
				&& !result.HasFlag(MoveResult.BlockedOnPositiveY);

			if ((moveY || !surfaced) && !result.HasFlag(MoveResult.Died))
			{
				hitBox.State = HitBoxState.Airborne;
			}
			else
			{
				hitBox.State = HitBoxState.Surfaced;
			}

			if (moveY && !result.HasFlag(MoveResult.Died))
			{
				aabb.Position.Y += interpolation.Y;
			}

			var reverse = CollisionDetection.CanMove(aabb.Bounds, -interpolation, DetectionType.Retrace);
			if (moveX) // fix issue when moving on X axis to the left.
			{
				if (reverse.HasFlag(MoveResult.BlockedOnNegativeX) && interpolation.X > 0)
				{
					aabb.Position.X -= interpolation.X;
					result &= ~MoveResult.X;
				}
				else if (reverse.HasFlag(MoveResult.BlockedOnPositiveX) && interpolation.X < 0)
				{
					aabb.Position.X -= interpolation.X;
					result &= ~MoveResult.X;
				}
			}

			if (moveY) // fix issue when hitting an impassable on Y axis when jumping.
			{
				if (reverse.HasFlag(MoveResult.BlockedOnNegativeY) && interpolation.Y > 0)
				{
					aabb.Position.Y -= interpolation.Y;
					result &= ~MoveResult.Y;
				}
				else if (reverse.HasFlag(MoveResult.BlockedOnPositiveY) && interpolation.Y < 0)
				{
					aabb.Position.Y -= interpolation.Y;
					result &= ~MoveResult.Y;
				}
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