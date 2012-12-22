using System.Collections.Generic;
using System.Linq;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class Background : IGameContent
	{
		private readonly IList<BackgroundAsset> assets;
		private readonly IList<BackgroundSprite> sprites;

		public Vector2 Position { get; private set; }

		public Background(IList<BackgroundAsset> assets)
		{
			this.assets = assets;
			sprites = new List<BackgroundSprite>();
		}

		public virtual void Initialize()
		{
			foreach (BackgroundAsset asset in assets)
			{
				var sprite = new BackgroundSprite(asset);
				sprite.Initialize();
				sprites.Add(sprite);
			}
		}

		public virtual void Load(ContentManager content)
		{
			foreach (BackgroundSprite sprite in sprites)
			{
				sprite.Load(content);
			}

			Arrange();
		}

		private void Arrange()
		{
			foreach (BackgroundSprite sprite in sprites)
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

			foreach (BackgroundSprite sprite in sprites)
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
			foreach (BackgroundSprite sprite in sprites)
			{
				// relativize the portion of background to the provided ScreenPosition.
				Vector2 relativeScreenPosition = screenPosition + sprite.Position;
				sprite.UpdateScreenPosition(relativeScreenPosition);
			}
		}
	}
}