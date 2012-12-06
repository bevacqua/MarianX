using MarianX.Mobiles;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public abstract class AxisAlignedBoundingBox
	{
		public abstract int HitBoxWidth { get; }
		public abstract int HitBoxHeight { get; }

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
			Vector2 center = CalculateOffset(mobile, mobile.Position, 1);
			Vector2 position = Offset(center, -1);
			Position = position;
		}

		public Vector2 GetPosition(Mobile mobile)
		{
			Vector2 center = Offset(Position, 1);
			Vector2 position = CalculateOffset(mobile, center, -1);
			return position;
		}

		private Vector2 CalculateOffset(Mobile mobile, Vector2 from, int multiplier)
		{
			float x = from.X + mobile.ContentWidth / 2.0f * multiplier;
			float y = from.Y + mobile.ContentHeight / 2.0f * multiplier;
			return new Vector2(x, y);
		}

		private Vector2 Offset(Vector2 source, int multiplier)
		{
			Vector2 position = source;
			position.X += OffsetX * multiplier;
			position.Y += OffsetY * multiplier;
			return position;
		}
	}
}