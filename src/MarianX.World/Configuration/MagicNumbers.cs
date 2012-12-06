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

		public const int MarianHitBoxWidth = 1;
		public const int MarianHitBoxHeight = 2;

		public const float StartX = 85;
		public const float StartY = 450;

		public static readonly Vector2 SpeedLimit = new Vector2(560, 680);
		public static readonly Vector2 MarianAcceleration = new Vector2(120, 180);

		public const int MarianJumpSpeed = 100;

		public static readonly TimeSpan MarianJumpWindow = TimeSpan.FromMilliseconds(450);
	}
}