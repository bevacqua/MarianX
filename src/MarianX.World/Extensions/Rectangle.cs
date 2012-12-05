using Microsoft.Xna.Framework;

namespace MarianX.World.Extensions
{
	public static class RectangleExtensions
	{
		public static Rectangle Offset(this Rectangle rectangle, Vector2 offset)
		{
			int x = rectangle.X + (int)offset.X;
			int y = rectangle.Y + (int)offset.Y;
			return new Rectangle(x, y, rectangle.Width, rectangle.Height);
		}
	}
}