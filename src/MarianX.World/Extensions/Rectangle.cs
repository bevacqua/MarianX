using Microsoft.Xna.Framework;

namespace MarianX.World.Extensions
{
	public static class RectangleExtensions
	{
		/// <summary>
		/// Extends a rectangle in the desired direction.
		/// </summary>
		public static Rectangle Extend(this Rectangle rectangle, Vector2 vector)
		{
			int x = (int) vector.X;
			if (x < 0)
			{
				rectangle.X -= x;
			}
			rectangle.Width += x;

			int y = (int)vector.Y;
			if (y < 0)
			{
				rectangle.Y -= y;
			}
			rectangle.Height += y;

			return rectangle;
		}
	}
}