using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public class NpcRecord
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Vector2 Position
		{
			get { return new Vector2(X, Y); }
		}
	}
}