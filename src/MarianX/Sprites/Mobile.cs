using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Enum;
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
			Vector2 interpolation = CalculateSpeedThroughAcceleration(gameTime);
			Vector2 gravity = GetGravityVector(gameTime);
			return interpolation + gravity;
		}

		private Vector2 CalculateSpeedThroughAcceleration(GameTime gameTime)
		{
			Vector2 targetSpeed = Direction.TargetSpeed;
			Vector2 direction = Direction.Sign(Speed);

			Vector2 updatedSpeed = Speed + Acceleration * direction;
			Speed = updatedSpeed.BoundBy(targetSpeed);

			float time = gameTime.GetElapsedSeconds();
			Vector2 interpolation = Speed * time;
			return interpolation;
		}

		// TODO: use acceleration for this as well.
		private Vector2 GetGravityVector(GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			
			if (Speed.Y < 0) // pull out of jump state.
			{
				Speed.Y += MagicNumbers.MarianGravityPullSpeed;
			}
			return MagicNumbers.Gravity * time;
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			MoveResult result = movement.Move(this, interpolation);

			if (result.HasFlag(MoveResult.Blocked)) // collided.
			{
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