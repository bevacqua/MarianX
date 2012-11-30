using MarianX.Platform;
using MarianX.Sprites;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class AxisAlignedBoundingBox
	{
		public int HitBoxWidth { get; protected set; }
		public int HitBoxHeight { get; protected set; }

		public Vector2 Position;

		public Rectangle Bounds
		{
			get
			{
				int x = (int)Position.X;
				int y = (int)Position.Y;
				int w = HitBoxWidth * Tile.Width;
				int h = HitBoxHeight * Tile.Height;
				return new Rectangle(x, y, w, h);
			}
		}

		protected int OffsetX
		{
			get { return HitBoxWidth * Tile.Width / 2; }
		}

		protected int OffsetY
		{
			get { return HitBoxHeight * Tile.Height / 2; }
		}

		public void UpdatePosition(Mobile mobile)
		{
			float x = mobile.Position.X + mobile.ContentWidth / 2.0f;
			float y = mobile.Position.Y + mobile.ContentHeight / 2.0f;
			Vector2 center = new Vector2(x, y);
			Vector2 position = OffsetPosition(center, -1);
			Position = position;
		}

		public Vector2 GetPosition(Mobile mobile)
		{
			Vector2 center = OffsetPosition(Position, 1);
			float x = center.X - mobile.ContentWidth / 2.0f;
			float y = center.Y - mobile.ContentHeight / 2.0f;
			return new Vector2(x, y);
		}

		private Vector2 OffsetPosition(Vector2 source, int multiplier)
		{
			Vector2 position = source;
			position.X += OffsetX * multiplier;
			position.Y += OffsetY * multiplier;
			return position;
		}
	}
}