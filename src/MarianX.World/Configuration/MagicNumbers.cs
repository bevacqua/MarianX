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

		// TODO: switch from accel to percentage base?
		public static readonly Vector2 Acceleration = new Vector2(25, 120);

		public static readonly Vector2 SpeedLimit = new Vector2(275, 480);
		public static readonly Vector2 GravityLimit = new Vector2(0, 680);

		public const int JumpSpeed = -350;
		public const float AerialSpeedPenaltyOnX = 4.3f;

		public static readonly TimeSpan JumpWindow = TimeSpan.FromMilliseconds(450);
	}
}