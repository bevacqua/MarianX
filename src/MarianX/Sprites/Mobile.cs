using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Extensions;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class Mobile : SpriteSheet, IHitBox
	{
		private readonly Movement movement;
		public Vector2 Acceleration { get; protected set; }

		public AxisAlignedBoundingBox BoundingBox { get; set; }

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
			Vector2 targetSpeed = Direction.TargetSpeed;
			Vector2 direction = Direction.Sign(Speed);

			Vector2 updatedSpeed = Speed + Acceleration * direction;
			Speed = updatedSpeed.BoundBy(targetSpeed);

			float time = gameTime.GetElapsedSeconds();
			Vector2 interpolation = Speed * time;
			return interpolation;
		}

		protected override void UpdatePosition(Vector2 interpolation)
		{
			bool moved = movement.Move(this, interpolation);
			if (moved)
			{
				position = BoundingBox.GetPosition(this);
			}
			else // collided.
			{
				Speed = Vector2.Zero;
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			base.Draw(gameTime, spriteBatch);
			spriteBatch.End();
		}
	}
}