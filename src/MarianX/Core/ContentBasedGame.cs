using System.Collections.Generic;
using System.Linq;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Core
{
	public class ContentBasedGame : Game
	{
		private readonly IList<IGameContent> contents;

		protected IList<IGameContent> Contents
		{
			get { return contents.ToList(); }
		}
		
		protected SpriteBatch spriteBatch { get; private set; }

		public bool HasContentLoaded { get; protected set; }

		public ContentBasedGame()
		{
			contents = new List<IGameContent>();
		}

		public virtual void ClearContent()
		{
			contents.Clear();
		}

		public virtual void AddContent(IGameContent content)
		{
			contents.Add(content);

			if (HasContentLoaded)
			{
				content.Load(Content);
			}
		}

		protected override void Initialize()
		{
			foreach (IGameContent content in Contents)
			{
				content.Initialize();
			}

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			foreach (IGameContent content in Contents)
			{
				content.Load(Content);
			}

			base.LoadContent();

			HasContentLoaded = true;
		}

		protected override void UnloadContent()
		{
			foreach (IGameContent content in Contents)
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
			foreach (IGameContent content in Contents)
			{
				content.UpdateInput(gameTime);
			}
		}

		protected virtual void UpdateOutput(GameTime gameTime)
		{
			foreach (IGameContent content in Contents)
			{
				content.UpdateOutput(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			foreach (IGameContent content in Contents)
			{
				content.Draw(gameTime, spriteBatch);
			}

			base.Draw(gameTime);
		}
	}
}