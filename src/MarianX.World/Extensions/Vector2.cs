using System;
using Microsoft.Xna.Framework;

namespace MarianX.World.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector2 Absolute(this Vector2 vector)
		{
			float x = Math.Abs(vector.X);
			float y = Math.Abs(vector.Y);
			return new Vector2(x, y);
		}

		public static Vector2 BoundBy(this Vector2 vector, Vector2 limit)
		{
			if (limit.X > 0 && vector.X > limit.X)
			{
				vector.X = limit.X;
			}
			else if (limit.X < 0 && vector.X < limit.X)
			{
				vector.X = limit.X;
			}

			if (limit.Y > 0 && vector.Y > limit.Y)
			{
				vector.Y = limit.Y;
			}
			else if (limit.Y < 0 && vector.Y < limit.Y)
			{
				vector.Y = limit.Y;
			}

			return vector;
		}
	}
}