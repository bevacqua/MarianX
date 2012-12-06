using System;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework;

namespace MarianX.Contents
{
	public sealed class Direction
	{
		public static Vector2 operator *(Direction x, Vector2 y)
		{
			return x.vector * y;
		}

		public static Vector2 operator *(Vector2 y, Direction x)
		{
			return x.vector * y;
		}

		public static readonly Direction None;
		public static readonly Direction Left;
		public static readonly Direction Right;
		public static readonly Direction Up;
		public static readonly Direction Down;

		static Direction()
		{
			None = new Direction(Vector2.Zero);
			Left = new Direction(new Vector2(-1, 0));
			Right = new Direction(new Vector2(1, 0));
			Up = new Direction(new Vector2(0, -1));
			Down = new Direction(new Vector2(0, 1), MagicNumbers.GravityLimit);
		}

		private readonly Vector2 vector;

		public Vector2 Limit { get; private set; }

		public Vector2 TargetSpeed
		{
			get { return vector * Limit; }
		}

		private Direction(Vector2 vector, Vector2? limit = null)
		{
			this.vector = vector;

			Limit = limit ?? MagicNumbers.SpeedLimit;
		}

		public Vector2 Sign(Vector2 currentSpeed)
		{
			float x = TargetSpeed.X - currentSpeed.X;
			float y = TargetSpeed.Y - currentSpeed.Y;

			Vector2 sign = new Vector2(Math.Sign(x), Math.Sign(y));
			return sign;
		}
	}
}