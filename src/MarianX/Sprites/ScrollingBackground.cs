using System.Collections.Generic;
using System.Linq;
using MarianX.Contents;
using MarianX.Core;
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

		public ScrollingBackground(IList<string> assetNames)
		{
			this.assetNames = assetNames;
			sprites = new List<Sprite>();
		}

		public void Initialize()
		{
			foreach (string assetName in assetNames)
			{
				Sprite sprite = new Sprite(assetName);
				sprites.Add(sprite);
			}

			// UpdateMovement(); // TODO: update alongside character, same with TileBackground
		}

		public void Load(ContentManager content)
		{
			Sprite previous = null;

			foreach (Sprite sprite in sprites)
			{
				sprite.Load(content);

				if (previous != null)
				{
					sprite.Position = new Vector2(previous.Position.X + previous.ActualSize.Width, previous.Position.Y);
				}
				previous = sprite;
			}
		}

		public void Update(GameTime gameTime)
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

		private void UpdateMovement()
		{
			Vector2 speed = new Vector2(160, 0);

			foreach (Sprite sprite in sprites)
			{
				sprite.Direction = Direction.Left;
				sprite.Speed = speed;
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			foreach (Sprite sprite in sprites)
			{
				sprite.Draw(gameTime, spriteBatch);
			}

			spriteBatch.End();
		}

		public void Unload()
		{
		}
	}
}