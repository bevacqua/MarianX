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

		public void UpdatePosition(Mobile mobile)
		{
			int x = mobile.ActualSize.Center.X - HitBoxWidth / 2;
			int y = mobile.ActualSize.Center.X - HitBoxWidth / 2;
			Position = new Vector2(x, y);
		}
	}
}