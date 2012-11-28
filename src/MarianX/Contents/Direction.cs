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

		public static readonly Direction Left;
		public static readonly Direction None;

		static Direction()
		{
			None = new Direction { Vector = Vector2.Zero };
			Left = new Direction { Vector = new Vector2(-1, 0) };
		}

		private Vector2 Vector { get; set; }

		private Direction()
		{
		}
	}
}