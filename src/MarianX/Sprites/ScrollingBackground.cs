using System;
using System.Collections.Generic;
using System.Linq;
using MarianX.Interface;
using MarianX.Sprites.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class ScrollingBackground : IGameContent
	{
		private readonly IList<ScrollingBackgroundAsset> assets;
		private readonly IList<ScrollingBackgroundSprite> sprites;

		public Vector2 Position { get; private set; }

		public ScrollingBackground(IList<ScrollingBackgroundAsset> assets)
		{
			this.assets = assets;
			sprites = new List<ScrollingBackgroundSprite>();
		}

		public virtual void Initialize()
		{
			foreach (ScrollingBackgroundAsset asset in assets)
			{
				var sprite = new ScrollingBackgroundSprite(asset);
				sprite.Initialize();
				sprites.Add(sprite);
			}

			Validate();
		}

		private void Validate()
		{
			// scrolling background makes a few assumptions, validate on them.
			bool width = sprites.All(s => sprites.All(sprite => sprite.ActualSize.Width == s.ActualSize.Width));
			bool height = sprites.All(s => sprites.All(sprite => sprite.ActualSize.Height == s.ActualSize.Height));

			if (!width || !height)
			{
				throw new InvalidOperationException(Error.ScrollingBackground_AssetDimensions);
			}
		}

		public virtual void Load(ContentManager content)
		{
			foreach (ScrollingBackgroundSprite sprite in sprites)
			{
				sprite.Load(content);
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			ScrollingBackgroundSprite previous = sprites.Last();

			foreach (ScrollingBackgroundSprite sprite in sprites)
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

			foreach (ScrollingBackgroundSprite sprite in sprites)
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
			foreach (ScrollingBackgroundSprite sprite in sprites)
			{
				sprite.ScreenPosition = screenPosition;
			}
		}
	}
}