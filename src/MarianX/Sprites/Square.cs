using System;
using MarianX.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class Square
	{
		private float alpha;

		public float Alpha
		{
			get { return alpha; }
			set
			{
				if (value < 0.0f || value > 1.0f)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				alpha = value;
			}
		}

		public Rectangle Bounds { get; set; }
		public Color Color { get; set; }

		public void Draw(SpriteBatch spriteBatch, Vector2 position)
		{
			GraphicsDevice device = GameCore.Instance.GraphicsDevice;
			Texture2D rectangle = new Texture2D(device, Bounds.Width, Bounds.Height);

			Color[] data = new Color[Bounds.Width * Bounds.Height];
			for (int i = 0; i < data.Length; ++i)
			{
				data[i] = Color;
			}

			rectangle.SetData(data);

			spriteBatch.Draw(rectangle, position, Color * Alpha);
		}
	}
}