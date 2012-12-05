using MarianX.World.Configuration;
using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public class Tile : TileType
	{
		public const int Width = MagicNumbers.TileWidth;
		public const int Height = MagicNumbers.TileHeight;

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
	}
}