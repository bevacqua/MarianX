using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class Content
	{
		private readonly string assetName;

		private Texture2D texture;
		private float scale;

		public Rectangle TextureSize;
		public Rectangle ActualSize;

		protected Content(string assetName)
		{
			this.assetName = assetName;

			Scale = 1.0f;
		}

		public Texture2D Texture
		{
			get { return texture; }
			private set
			{
				texture = value;
				InvalidateTextureScale();
			}
		}

		public float Scale
		{
			get { return scale; }
			protected set
			{
				scale = value;
				InvalidateTextureScale();
			}
		}

		public virtual int ContentWidth { get { return TextureSize.Width; } }
		public virtual int ContentHeight { get { return TextureSize.Height; } }

		private void InvalidateTextureScale()
		{
			if (texture == null)
			{
				TextureSize = Rectangle.Empty;
				ActualSize = Rectangle.Empty;
			}
			else
			{
				TextureSize = Texture.Bounds;
				ActualSize = new Rectangle(0, 0, (int)(ContentWidth * Scale), (int)(ContentHeight * Scale));
			}
		}

		public virtual void Load(ContentManager content)
		{
			Texture = content.Load<Texture2D>(assetName);
		}
	}
}