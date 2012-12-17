using System;
using MarianX.Mobiles;
using MarianX.Sprites;
using MarianX.World.Configuration;
using MarianX.World.Extensions;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class InterpolationCalculator
	{
		private readonly Mobile mobile;

		public InterpolationCalculator(Mobile mobile)
		{
			this.mobile = mobile;
		}

		public virtual Vector2 CalculateInterpolation(GameTime gameTime)
		{
			Vector2 velocity = CalculateSpeedOnDirection(mobile.Direction, gameTime);
			Vector2 gravity = CalculateGravitySpeed(gameTime);

			Vector2 target = mobile.Speed + gravity + velocity;

			Vector2 afterHorizontal = HorizontalSpeedChanges(target, velocity);
			Vector2 afterVertical = VerticalSpeedChanges(afterHorizontal, gravity);

			mobile.Speed = ConstrainSpeed(afterVertical);

			float time = gameTime.GetElapsedSeconds();
			return mobile.Speed * time;
		}

		public Vector2 CalculateGravitySpeed(GameTime gameTime)
		{
			return CalculateSpeedOnDirection(Direction.Down, gameTime);
		}

		private Vector2 CalculateSpeedOnDirection(Direction direction, GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			Vector2 accel = direction.Acceleration;

			if (mobile.State == HitBoxState.Airborne)
			{
				accel.X /= MagicNumbers.AerialAccelerationPenaltyOnX;
			}

			Vector2 velocity = accel * direction * (float)Math.Pow(time, 2);
			return velocity;
		}

		private Vector2 HorizontalSpeedChanges(Vector2 target, Vector2 velocity)
		{
			if (target.X + velocity.X > 0 && mobile.Direction == Direction.Left ||
				target.X + velocity.X < 0 && mobile.Direction == Direction.Right)
			{
				target.X /= MagicNumbers.DirectionChangeSpeedPenalty;
			}

			if (mobile.State == HitBoxState.Surfaced && mobile.Direction == Direction.None && target.X != 0)
			{
				if (target.X < 0)
				{
					target.X = Math.Min(target.X + MagicNumbers.Friction, 0);

				}
				else if (target.X > 0)
				{
					target.X = Math.Max(target.X - MagicNumbers.Friction, 0);
				}
			}

			return target;
		}

		private Vector2 VerticalSpeedChanges(Vector2 target, Vector2 gravity)
		{
			if (mobile.State == HitBoxState.Surfaced && gravity.Y > 0)
			{
				target -= gravity;
			}
			return target;
		}

		private Vector2 ConstrainSpeed(Vector2 target)
		{
			Vector2 negative = MagicNumbers.NegativeLimit;
			Vector2 positive = MagicNumbers.PositiveLimit;

			if (mobile.State == HitBoxState.Airborne)
			{
				negative.X /= MagicNumbers.AerialSpeedPenaltyOnX;
				positive.X /= MagicNumbers.AerialSpeedPenaltyOnX;
			}

			Vector2 constrained = target.Constrained(negative, positive);
			return constrained;
		}
	}
}