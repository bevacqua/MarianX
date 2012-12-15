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

		public static readonly Vector2 PositiveLimit = new Vector2(500, 450);
		public static readonly Vector2 NegativeLimit = new Vector2(-500, -450);

		public static readonly Vector2 DefaultAcceleration = new Vector2(8000, 0);
		public static readonly Vector2 GravityAcceleration = new Vector2(0, 10000);

		public const int JumpSpeed = -295;
		public const float AerialSpeedPenaltyOnX = 0.75f;
		public const float AerialAccelerationPenaltyOnX = 0.75f;

		public static readonly TimeSpan JumpWindow = TimeSpan.FromMilliseconds(400);

		public const float DirectionChangeSpeedPenalty = 1.1f;

		public const float Friction = 4.4f;

		public const int RelativeScreenLeft = 288;
		public const int RelativeScreenRight = 288;
		public const int RelativeScreenTop = 144;
		public const int RelativeScreenBottom = 144;

		public static readonly TimeSpan Flash = TimeSpan.FromSeconds(2.8);
		public static readonly Color FlashTint = Color.LightGray;
		public const int FlashFrame = 7;

		public static readonly Vector2 FallEffect = new Vector2(0, 24);
	}
}