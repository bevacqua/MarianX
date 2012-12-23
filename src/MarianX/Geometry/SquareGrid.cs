using System.Collections.Generic;
using System.Linq;
using MarianX.Core;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Geometry
{
	/// <summary>
	/// Merges a list of squares into a single draw call, drastically improving render performance.
	/// </summary>
	public class SquareGrid : ISquareGrid
	{
		private readonly IList<Square> squares;

		private Texture2D texture;

		public SquareGrid(IList<Square> squares)
		{
			this.squares = squares;
		}

		private Texture2D GetTexture()
		{
			if (squares.Count == 0)
			{
				return null;
			}
			int w = squares.Max(s => s.Bounds.X + s.Bounds.Width);
			int h = squares.Max(s => s.Bounds.Y + s.Bounds.Height);
			GraphicsDevice device = GameCore.Instance.GraphicsDevice;
			Texture2D graphic = new Texture2D(device, w, h);

			foreach (Square square in squares)
			{
				Color[] data = square.GetData();
				graphic.SetData(0, square.Bounds, data, 0, data.Length);
			}
			return graphic;
		}

		public virtual void Draw(SpriteBatch spriteBatch, Vector2? offset = null)
		{
			if (squares.Count == 0)
			{
				return;
			}
			if (texture == null)
			{
				texture = GetTexture();
			}
			spriteBatch.Draw(texture, offset ?? Vector2.Zero, Color.White);
		}
	}
}