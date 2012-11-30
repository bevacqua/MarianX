using MarianX.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class AxisAlignedBoundingBox
	{
		public const int TilesWide = 1;
		public const int TilesHigh = 2;

		public Vector2 Position;

		public Rectangle Bounds
		{
			get
			{
				int x = (int)Position.X;
				int y = (int)Position.Y;
				int w = TilesWide * Tile.Width;
				int h = TilesHigh * Tile.Height;
				return new Rectangle(x, y, w, h);
			}
		}
	}
}