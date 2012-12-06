using System;
using MarianX.Contents;
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

			Vector2 target = mobile.Speed + gravity + velocity;

			mobile.Speed = ConstrainSpeed(target);

			float time = gameTime.GetElapsedSeconds();
			return mobile.Speed * time;
		}

		private Vector2 CalculateSpeedOnDirection(Direction direction, GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			Vector2 accel = direction.Acceleration;

			if (mobile.State == HitBoxState.Airborne)
			{
				accel.X /= MagicNumbers.AerialAccelerationPenaltyOnX;
			}

			return accel * direction * (float)Math.Pow(time, 2);
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