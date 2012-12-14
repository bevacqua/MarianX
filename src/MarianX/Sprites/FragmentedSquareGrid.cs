using System;
using System.Collections.Generic;
using System.Linq;
using MarianX.Core;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class FragmentedSquareGrid : ISquareGrid
	{
		const int fragmentWidth = 4000;
		const int fragmentHeight = 4000;

		private readonly IList<Square> squares;
		private IList<TextureFragment> fragments;

		public FragmentedSquareGrid(IList<Square> squares)
		{
			this.squares = squares;
		}

		private IList<TextureFragment> GetTextureFragments()
		{
			if (squares.Count == 0)
			{
				return null;
			}
			int totalWidth = squares.Max(s => s.Bounds.X + s.Bounds.Width);
			int totalHeight = squares.Max(s => s.Bounds.Y + s.Bounds.Height);

			IList<TextureFragment> fragments = new List<TextureFragment>();

			int remainingWidth = totalWidth;

			while (remainingWidth > 0)
			{
				int x = totalWidth - remainingWidth;
				int w = remainingWidth;
				if (w > fragmentWidth)
				{
					w = fragmentWidth;
				}
				remainingWidth -= w;

				int remainingHeight = totalHeight;

				while (remainingHeight > 0)
				{
					int y = totalHeight - remainingHeight;
					int h = remainingHeight;
					if (h > fragmentHeight)
					{
						h = fragmentHeight;
					}
					remainingHeight -= h;

					GraphicsDevice device = GameCore.Instance.GraphicsDevice;
					Texture2D t = new Texture2D(device, w, h);
					Vector2 r = new Vector2(x, y);
					TextureFragment fragment = new TextureFragment
					{
						Texture = t,
						Relative = r
					};
					fragments.Add(fragment);

					foreach (Square square in squares)
					{
						Rectangle s = square.Bounds;
						Rectangle rect = new Rectangle(s.X - (int)r.X, s.Y - (int)r.Y, s.Width, s.Height);

						if (rect.X + rect.Width > x + w || rect.Y + rect.Height > y + h)
						{
							continue;
						}

						if (rect.X < 0 || rect.Y < 0)
						{
							continue;
						}

						Color[] data = square.GetData();
						t.SetData(0, rect, data, 0, data.Length);
					}
				}
			}

			return fragments;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2? offset = null)
		{
			if (squares.Count == 0)
			{
				return;
			}
			if (fragments == null)
			{
				fragments = GetTextureFragments();
			}
			foreach (TextureFragment fragment in fragments)
			{
				Vector2 position = fragment.Relative + (offset ?? Vector2.Zero);
				spriteBatch.Draw(fragment.Texture, position, Color.White);
			}
		}

		private sealed class TextureFragment
		{
			public Texture2D Texture { get; set; }
			public Vector2 Relative { get; set; }
		}
	}
}