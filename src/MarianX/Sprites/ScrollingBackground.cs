using System.Collections.Generic;
using System.Linq;
using MarianX.Interface;
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
		}

		public virtual void Load(ContentManager content)
		{
			foreach (ScrollingBackgroundSprite sprite in sprites)
			{
				sprite.Load(content);
			}

			Arrange();
		}

		private void Arrange()
		{
			foreach (ScrollingBackgroundSprite sprite in sprites)
			{
				int x = sprites.Where(s => 
					s.Asset.X < sprite.Asset.X && 
					s.Asset.Y == sprite.Asset.Y
				).Sum(s => s.ActualSize.Width);

				int y = sprites.Where(s => 
					s.Asset.Y < sprite.Asset.Y && 
					s.Asset.X == sprite.Asset.X
				).Sum(s => s.ActualSize.Height);
				
				sprite.Position = new Vector2(x, y);
			}
		}

		public virtual void UpdateInput(GameTime gameTime)
		{
		}

		public virtual void UpdateOutput(GameTime gameTime)
		{
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
				// relativize the portion of background to the provided ScreenPosition.
				Vector2 relativeScreenPosition = screenPosition + sprite.Position;
				sprite.UpdateScreenPosition(relativeScreenPosition);
			}
		}
	}
}