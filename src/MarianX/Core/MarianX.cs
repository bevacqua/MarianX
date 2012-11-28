using System.Collections.Generic;
using MarianX.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class MarianX : ContentBasedGame
	{
		public MarianX()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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

			AddContent(background);
			
			base.Initialize();
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

			base.Update(gameTime);
		}
	}
}
