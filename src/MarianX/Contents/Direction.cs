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
			Down = new Direction(new Vector2(0, 1));
		}

		private Vector2 Vector { get; set; }

		private Direction(Vector2 vector)
		{
			Vector = vector;
		}
	}
}