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
			Texture2D texture = new Texture2D(device, w, h);

			foreach (Square square in squares)
			{
				Color[] data = square.GetData();
				texture.SetData(0, square.Bounds, data, 0, data.Length);
			}

			return texture; // so that it can be cached.
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 position)
		{
			if (texture == null)
			{
				texture = GetTexture();
			}
			float alpha = squares.Average(s => s.Alpha);
			spriteBatch.Draw(texture, position, Color.White * alpha);
		}
	}
}