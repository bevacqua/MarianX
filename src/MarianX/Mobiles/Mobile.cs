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
			Acceleration = MagicNumbers.Acceleration;
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			Vector2 gravity = CalculateSpeedOnDirection(Direction.Down, gameTime, false);
			Vector2 interpolation = CalculateSpeedOnDirection(Direction, gameTime, true);

			return interpolation + gravity;
		}

		private Vector2 CalculateSpeedOnDirection(Direction direction, GameTime gameTime, bool assign)
		{
			Vector2 targetSpeed = direction.TargetSpeed;
			Vector2 sign = direction.Sign(Speed);

			if (State == HitBoxState.Airborne)
			{
				targetSpeed.X /= MagicNumbers.AerialSpeedPenaltyOnX;
			}

			Vector2 updatedSpeed = Speed + Acceleration * sign;
			Vector2 limitedSpeed = updatedSpeed.BoundBy(targetSpeed);

			if (assign)
			{
				Speed = limitedSpeed;
			}
			float time = gameTime.GetElapsedSeconds();
			return limitedSpeed * time;
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