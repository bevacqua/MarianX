using System;
using MarianX.Configuration;
using MarianX.Interface;
using Microsoft.Xna.Framework;

namespace MarianX.Contents
{
	public sealed class Direction
	{
		public static Vector2 operator *(Direction x, Vector2 y)
		{
			return x.Vector * y;
		}

		public static Vector2 operator *(Vector2 y, Direction x)
		{
			return x.Vector * y;
		}

		private static readonly Vector2 SpeedLimit;

		public static readonly Direction None;
		public static readonly Direction Left;
		public static readonly Direction Right;
		public static readonly Direction Up;
		public static readonly Direction Down;

		static Direction()
		{
			SpeedLimit = MagicNumbers.SpeedLimit;
			None = new Direction(Vector2.Zero);
			Left = new Direction(new Vector2(-1, 0));
			Right = new Direction(new Vector2(1, 0));
			Up = new Direction(new Vector2(0, -1));
			Down = new Direction(new Vector2(0, 1));
		}

		private Vector2 vector;

		private Vector2 Vector
		{
			get { return vector; }
			set
			{
				vector = value;
				TargetSpeed = value * SpeedLimit;
			}
		}

		public Vector2 TargetSpeed { get; private set; }

		private Direction(Vector2 vector)
		{
			Vector = vector;
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