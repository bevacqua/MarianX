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
			Vector2 gravity = CalculateSpeedOnDirection(Direction.Down, gameTime);

			HorizontalSpeedChanges(velocity);

			Vector2 target = mobile.Speed + gravity + velocity;

			mobile.Speed = ConstrainSpeed(target);

			float time = gameTime.GetElapsedSeconds();
			return mobile.Speed * time;
		}

		private void HorizontalSpeedChanges(Vector2 velocity)
		{
			if (mobile.Speed.X + velocity.X > 0 && mobile.Direction == Direction.Left ||
				mobile.Speed.X + velocity.X < 0 && mobile.Direction == Direction.Right)
			{
				mobile.Speed.X /= MagicNumbers.DirectionChangeSpeedPenalty;
			}

			if (mobile.State == HitBoxState.Surfaced && mobile.Direction == Direction.None && mobile.Speed.X != 0)
			{
				if (mobile.Speed.X < 0)
				{
					mobile.Speed.X = Math.Min(mobile.Speed.X + MagicNumbers.Friction, 0);

				}
				else if (mobile.Speed.X > 0)
				{
					mobile.Speed.X = Math.Max(mobile.Speed.X - MagicNumbers.Friction, 0);
				}
			}
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