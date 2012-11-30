using Microsoft.Xna.Framework;

namespace MarianX.Extensions
{
	public static class RectangleExtensions
	{
		public static void Offset(this Rectangle rectangle, Vector2 offset)
		{
			rectangle.Offset((int)offset.X, (int)offset.Y);
		}
	}
}