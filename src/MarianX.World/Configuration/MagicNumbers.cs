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

		public static readonly Vector2 PositiveLimit = new Vector2(275, 240);
		public static readonly Vector2 NegativeLimit = new Vector2(-275, -420);

		public static readonly Vector2 DefaultAcceleration = new Vector2(4000, 2000);
		public static readonly Vector2 GravityAcceleration = new Vector2(0, 8000);

		public const int JumpSpeed = -420;
		public const float AerialSpeedPenaltyOnX = 2.5f;
		public const float AerialAccelerationPenaltyOnX = 1.5f;

		public static readonly TimeSpan JumpWindow = TimeSpan.FromMilliseconds(650);
	}
}