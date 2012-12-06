using System;
using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.Sprites;
using MarianX.World.Configuration;
using MarianX.World.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Mobiles
{
	public class Mobile : SpriteSheet, IHitBox
	{
		private readonly Movement movement;

		public AxisAlignedBoundingBox BoundingBox { get; protected set; }
		public HitBoxState State { get; set; }

		private Vector2 position;

		public override Vector2 Position
		{
			get { return position; }
			set
			{
				position = value;
				BoundingBox.UpdatePosition(this);
			}
		}

		public Mobile(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			movement = new Movement(new CollisionDetection());
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			Vector2 velocity = CalculateSpeedOnDirection(Direction, gameTime);
			Vector2 gravity = CalculateSpeedOnDirection(Direction.Down, gameTime);

			Vector2 target = Speed + gravity + velocity;

			Speed = ConstrainSpeed(target);

			float time = gameTime.GetElapsedSeconds();
			return Speed * time;
		}

		private Vector2 CalculateSpeedOnDirection(Direction direction, GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			Vector2 accel = direction.Acceleration;

			if (State == HitBoxState.Airborne)
			{
				accel.X /= MagicNumbers.AerialAccelerationPenaltyOnX;
			}

			return accel * direction * (float)Math.Pow(time, 2);
		}

		private Vector2 ConstrainSpeed(Vector2 target)
		{
			Vector2 negative = MagicNumbers.NegativeLimit;
			Vector2 positive = MagicNumbers.PositiveLimit;

			if (State == HitBoxState.Airborne)
			{
				negative.X /= MagicNumbers.AerialSpeedPenaltyOnX;
				positive.X /= MagicNumbers.AerialSpeedPenaltyOnX;
			}

			Vector2 constrained = target.Constrained(negative, positive);
			return constrained;
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			MoveResult result = movement.Move(this, interpolation);

			if (result.HasFlag(MoveResult.Blocked)) // collided.
			{
				Direction = Direction.None;
				Speed = Vector2.Zero;
			}
			else
			{
				position = BoundingBox.GetPosition(this);
			}
			return result;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			base.Draw(gameTime, spriteBatch);
			spriteBatch.End();
		}
	}
}