using System.Collections.Generic;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Core
{
	public class ContentBasedGame : Game
	{
		private readonly IList<IGameContent> contents;

		protected SpriteBatch spriteBatch { get; private set; }

		public ContentBasedGame()
		{
			contents = new List<IGameContent>();
		}

		protected virtual void AddContent(IGameContent gameContent)
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
			UpdateInput(gameTime);
			UpdateOutput(gameTime);

			base.Update(gameTime);
		}

		protected virtual void UpdateInput(GameTime gameTime)
		{
			foreach (IGameContent content in contents)
			{
				content.UpdateInput(gameTime);
			}
		}

		protected virtual void UpdateOutput(GameTime gameTime)
		{
			foreach (IGameContent content in contents)
			{
				content.UpdateOutput(gameTime);
			}
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