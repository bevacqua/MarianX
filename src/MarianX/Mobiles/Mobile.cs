using MarianX.Contents;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.Physics;
using MarianX.Sprites;
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
			movement = new Movement(new CollisionDetection(), new InterpolationCalculator(this));
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			return movement.Interpolated(gameTime);
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