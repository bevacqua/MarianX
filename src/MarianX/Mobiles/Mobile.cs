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
		public Movement Movement { get; protected set; }

		public AxisAlignedBoundingBox BoundingBox { get; protected set; }
		public virtual HitBoxState State { get; set; }

		public override Vector2 Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				if (Position != value)
				{
					Vector2 oldPosition = Position;
					base.Position = value;
					BoundingBox.UpdatePosition(this);
					RaiseMoved(oldPosition, value);
				}
			}
		}

		public event Move Move;

		private void RaiseMoved(Vector2 oldPosition, Vector2 currentPosition)
		{
			if (Move != null)
			{
				Move(this, new MoveEventArgs
				{
					From = oldPosition,
					To = currentPosition
				});
			}
		}

		public Mobile(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			Movement = new Movement(new DiagnosticCollisionDetection(), new InterpolationCalculator(this));
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			return Movement.Interpolated(gameTime);
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			MoveResult result = Movement.Move(this, interpolation);

			if (result.HasFlag(MoveResult.Blocked)) // collided.
			{
				Direction = Direction.None;
				Speed = Vector2.Zero;
			}
			else
			{
				Position = BoundingBox.GetPosition(this);
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