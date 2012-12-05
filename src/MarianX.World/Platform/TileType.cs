using Microsoft.Xna.Framework.Audio;

namespace MarianX.World.Platform
{
	public class TileType
	{
		public string Type { get; set; }
		public bool Impassable { get; set; }

		public int SlopeLeft { get; set; }
		public int SlopeRight { get; set; }

		public SoundEffect Sound { get; set; }
	}
}