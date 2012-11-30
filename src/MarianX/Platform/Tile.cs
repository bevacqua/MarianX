using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MarianX.Platform
{
	public class Tile
	{
		public const int Width = 24;
		public const int Height = 24;

		public bool Impassable { get; set; }

		public Vector2 Position { get; set; }

		public int SlopeLeft { get; set; }
		public int SlopeRight { get; set; }

		public SoundEffect Sound { get; set; }
	}
}