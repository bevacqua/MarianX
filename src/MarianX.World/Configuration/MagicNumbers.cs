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

		public static readonly Vector2 PositiveLimit = new Vector2(500, 450);
		public static readonly Vector2 NegativeLimit = new Vector2(-500, -450);

		public static readonly Vector2 DefaultAcceleration = new Vector2(8000, 0);
		public static readonly Vector2 GravityAcceleration = new Vector2(0, 13000);

		public const int JumpSpeed = -390;
		public const float AerialSpeedPenaltyOnX = 1.45f;
		public const float AerialAccelerationPenaltyOnX = 1.30f;

		public static readonly TimeSpan JumpWindow = TimeSpan.FromMilliseconds(400);

		public const float DirectionChangeSpeedPenalty = 1.1f;

		public const float Friction = 4.4f;
		
		public static readonly TimeSpan InvulnerableTimeout = TimeSpan.FromSeconds(4.5);
		public static readonly Color InvulnerableTint = Color.LightGray;
		public const int InvulnerableFrameInterval = 7;

		public static readonly Vector2 FallEffect = new Vector2(0, 24);

		public const float AcceptableSurfaceDistance = 4;

		public const int GloopFrameWidth = 64;
		public const int GloopFrameHeight = 48;

		public const int GloopHitBoxWidth = 2;
		public const int GloopHitBoxHeight = 1;

		public static readonly Vector2 GloopInterpolationMargin = new Vector2(GloopFrameWidth, 0);
		public static readonly Vector2 GloopVertigoMargin = new Vector2(0, 20);

		public const double GameSpeedPower = 1.85;

		public const int JailFrameWidth = 120;
		public const int JailFrameHeight = 96;

		public const int JailHitBoxWidth = 3;
		public const int JailHitBoxHeight = 2;
	}
}