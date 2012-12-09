using System;
using Microsoft.Xna.Framework;

namespace MarianX.World.Configuration
{
	public class MagicNumbers
	{
		public const int TileWidth = 24;
		public const int TileHeight = 24;

		public const int FrameWidth = 48;
		public const int FrameHeight = 72;

		public const int HitBoxWidth = 1;
		public const int HitBoxHeight = 2;

		public const float StartX = 85;
		public const float StartY = 450;

		public static readonly Vector2 PositiveLimit = new Vector2(500, 350);
		public static readonly Vector2 NegativeLimit = new Vector2(-500, -320);

		public static readonly Vector2 DefaultAcceleration = new Vector2(8000, 0);
		public static readonly Vector2 GravityAcceleration = new Vector2(0, 10000);

		public const int JumpSpeed = -210;
		public const float AerialSpeedPenaltyOnX = 1.3f;
		public const float AerialAccelerationPenaltyOnX = 0.75f;

		public static readonly TimeSpan JumpWindow = TimeSpan.FromMilliseconds(400);

		public const float DirectionChangeSpeedPenalty = 1.1f;

		public const float Friction = 4.4f;
	}
}