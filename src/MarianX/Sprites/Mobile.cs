using System;
using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Enum;
using MarianX.Events;
using MarianX.Interface;
using MarianX.World.Configuration;
using MarianX.World.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class Mobile : SpriteSheet, IHitBox
	{
		private readonly Movement movement;
		public Vector2 Acceleration { get; protected set; }

		public AxisAlignedBoundingBox BoundingBox { get; protected set; }
		public HitBoxState State { get; set; }

		public event OnStaticCollision OnStaticCollision;

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
			Acceleration = MagicNumbers.MarianAcceleration;
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			Vector2 gravity = CalculateInterpolationInDirection(Direction.Down, gameTime);
			Vector2 interpolation = CalculateInterpolationInDirection(Direction, gameTime);
			return interpolation + gravity;
		}

		private Vector2 CalculateInterpolationInDirection(Direction direction, GameTime gameTime)
		{
			Vector2 targetSpeed = direction.TargetSpeed;
			Vector2 sign = direction.Sign(Speed);

			Vector2 updatedSpeed = Speed + Acceleration * sign;
			Vector2 limitedSpeed = updatedSpeed.BoundBy(targetSpeed);
			Speed = limitedSpeed;

			float time = gameTime.GetElapsedSeconds();
			Vector2 interpolation = limitedSpeed * time;
			return interpolation;
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			MoveResult result = movement.Move(this, interpolation);

			if (result.HasFlag(MoveResult.Blocked)) // collided.
			{
				Direction = Direction.None;
				Speed = Vector2.Zero;
				RaiseStaticCollision(this, new EventArgs());
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

		protected void RaiseStaticCollision(object sender, EventArgs args)
		{
			if (OnStaticCollision != null)
				OnStaticCollision(sender, args);
		}
	}
}