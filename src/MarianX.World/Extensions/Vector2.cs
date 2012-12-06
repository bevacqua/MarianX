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

		public static Vector2 Constrained(this Vector2 vector, Vector2 negative, Vector2 positive)
		{
			if (vector.X < negative.X)
			{
				vector.X = negative.X;
			}
			else if (vector.X > positive.X)
			{
				vector.X = positive.X;
			}

			if (vector.Y < negative.Y)
			{
				vector.Y = negative.Y;
			}
			else if (vector.Y > positive.Y)
			{
				vector.Y = positive.Y;
			}

			return vector;
		}
	}
}