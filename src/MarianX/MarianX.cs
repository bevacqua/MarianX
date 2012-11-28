using System.Collections.Generic;
using MarianX.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarianX
{
	public class MarianX : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private readonly IList<IGameContent> contents;

		public MarianX()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			contents = new List<IGameContent>();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			IList<string> backgroundAssets = new[]
			{
				"Backgrounds/congress",
				"Backgrounds/eiffel",
				"Backgrounds/island",
				"Backgrounds/louvre",
				"Backgrounds/ny",
				"Backgrounds/opera",
				"Backgrounds/panama"
			};
			var background = new ScrollingBackground(backgroundAssets);

			contents.Add(background);

			foreach (IGameContent content in contents)
			{
				content.Initialize();
			}

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

			foreach (IGameContent content in contents)
			{
				content.Load(Content);
			}

			base.LoadContent();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			foreach (IGameContent content in contents)
			{
				content.Unload();
			}
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}

			foreach (IGameContent content in contents)
			{
				content.Update(gameTime);
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			foreach (IGameContent content in contents)
			{
				content.Draw(gameTime, spriteBatch);
			}

			base.Draw(gameTime);
		}
	}
}
