using System.Collections.Generic;
using System.Linq;
using MarianX.Contents;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class ScrollingBackground : IGameContent
	{
		private readonly IList<string> assetNames;
		private readonly IList<Sprite> sprites;

		public Vector2 Position { get; private set; }

		public ScrollingBackground(IList<string> assetNames)
		{
			this.assetNames = assetNames;
			sprites = new List<Sprite>();
		}

		public virtual void Initialize()
		{
			foreach (string assetName in assetNames)
			{
				Sprite sprite = new Sprite(assetName);
				sprite.Initialize();
				sprites.Add(sprite);
			}
		}

		public virtual void Load(ContentManager content)
		{
			Sprite previous = null;

			foreach (Sprite sprite in sprites)
			{
				sprite.Load(content);

				if (previous == null)
				{
					Position = Vector2.Zero;
				}
				else
				{
					sprite.Position = new Vector2(previous.Position.X + previous.ActualSize.Width, previous.Position.Y);
				}
				previous = sprite;
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			Sprite previous = sprites.Last();

			foreach (Sprite sprite in sprites)
			{
				if (sprite.Position.X < -sprite.ActualSize.Width)
				{
					float x = previous.Position.X + previous.ActualSize.Width;
					sprite.Position = new Vector2(x, sprite.Position.Y);
				}

				sprite.Update(gameTime);
				previous = sprite;
			}
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			foreach (Sprite sprite in sprites)
			{
				sprite.Draw(gameTime, spriteBatch);
			}

			spriteBatch.End();
		}

		public virtual void Unload()
		{
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
			foreach (Sprite sprite in sprites)
			{
				sprite.ScreenPosition = screenPosition;
			}
		}
	}
}