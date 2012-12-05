using System;
using Microsoft.Xna.Framework;

namespace MarianX.World.Configuration
{
	public class MagicNumbers
	{
		public const int TileWidth = 24;
		public const int TileHeight = 24;

		public const int MarianFrameWidth = 48;
		public const int MarianFrameHeight = 72;

		public const int MarianHitBoxWidth = 1;
		public const int MarianHitBoxHeight = 2;

		public const float StartX = 125;

		public static readonly Vector2 SpeedLimit = new Vector2(320, 1024);
		public static readonly Vector2 MarianAcceleration = new Vector2(120, 320);

		public static readonly Vector2 Gravity = new Vector2(0, 110);

		public const int MarianJumpSpeed = 30;
		public const int MarianGravityPullSpeed = -3;

		public static readonly TimeSpan MarianJumpWindow = TimeSpan.FromMilliseconds(200);
	}
}