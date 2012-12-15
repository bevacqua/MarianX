using MarianX.World.Configuration;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public sealed class Direction
	{
		public static readonly Direction None = new Direction(Vector2.Zero);
		public static readonly Direction Left = new Direction(new Vector2(-1, 0));
		public static readonly Direction Right = new Direction(new Vector2(1, 0));
		public static readonly Direction Up = new Direction(new Vector2(0, -1));
		public static readonly Direction Down = new Direction(new Vector2(0, 1), MagicNumbers.GravityAcceleration);

		public static Vector2 operator *(Direction x, Vector2 y)
		{
			return x.Vector * y;
		}

		public static Vector2 operator *(Vector2 y, Direction x)
		{
			return x.Vector * y;
		}

		public Vector2 Vector { get; private set; }
		public Vector2 Acceleration { get; private set; }

		private Direction(Vector2 vector, Vector2? acceleration = null)
		{
			Vector = vector;
			Acceleration = acceleration ?? MagicNumbers.DefaultAcceleration;
		}
	}
}