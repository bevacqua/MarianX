using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class Mobile : SpriteSheet, IHitBox
	{
		private readonly Movement movement;

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
			movement = new Movement();
		}

		public override void UpdatePosition(Vector2 interpolation)
		{
			bool moved = movement.Move(this, interpolation);
			if (moved)
			{
				position = BoundingBox.GetPosition(this); // don't invalidate.
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