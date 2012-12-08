using System.Collections.Generic;
using System.Linq;
using MarianX.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	/// <summary>
	/// Merges a list of squares into a single draw call, drastically improving render performance.
	/// </summary>
	public class SquareGrid
	{
		private readonly IList<Square> squares;

		private Texture2D texture;

		public SquareGrid(IList<Square> squares)
		{
			this.squares = squares;
		}

		private Texture2D GetTexture()
		{
			GraphicsDevice device = GameCore.Instance.GraphicsDevice;
			Square last = squares.Last(); // assume the last square is actually the furthest one.

			int w = last.Bounds.X + last.Bounds.Width;
			int h = last.Bounds.Y + last.Bounds.Height;
			Texture2D graphic = new Texture2D(device, w, h);

			foreach (Square square in squares)
			{
				Color[] data = square.GetData();
				graphic.SetData(0, square.Bounds, data, 0, data.Length);
			}
			return graphic;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2? position = null)
		{
			if (texture == null)
			{
				texture = GetTexture();
			}
			spriteBatch.Draw(texture, position ?? Vector2.Zero, Color.White);
		}
	}
}