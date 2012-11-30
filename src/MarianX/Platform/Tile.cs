using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MarianX.Platform
{
	public class Tile
	{
		public const int Width = MagicNumbers.TileWidth;
		public const int Height = MagicNumbers.TileHeight;

		public bool Impassable { get; set; }

		private Point position;

		public Point Position
		{
			get { return position; }
			set
			{
				position = value;
				Bounds = new Rectangle(position.X, position.Y, Width, Height);
			}
		}

		public Rectangle Bounds { get; private set; }

		public int SlopeLeft { get; set; }
		public int SlopeRight { get; set; }

		public SoundEffect Sound { get; set; }
	}
}