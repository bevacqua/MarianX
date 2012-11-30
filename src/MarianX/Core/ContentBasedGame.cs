using System.Collections.Generic;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Core
{
	public class ContentBasedGame : Game
	{
		private readonly IList<IGameContent> contents;
		private SpriteBatch spriteBatch;

		public ContentBasedGame()
		{
			contents = new List<IGameContent>();
		}

		protected void AddContent(IGameContent gameContent)
		{
			contents.Add(gameContent);
		}

		protected override void Initialize()
		{
			foreach (IGameContent content in contents)
			{
				content.Initialize();
			}

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			foreach (IGameContent content in contents)
			{
				content.Load(Content);
			}

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			foreach (IGameContent content in contents)
			{
				content.Unload();
			}

			base.UnloadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			foreach (IGameContent content in contents)
			{
				content.Update(gameTime);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			foreach (IGameContent content in contents)
			{
				content.Draw(gameTime, spriteBatch);
			}

			base.Draw(gameTime);
		}
	}
}