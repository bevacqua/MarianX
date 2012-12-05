using MarianX.Sprites;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class GameCore : ContentBasedGame
	{
		public static GameCore Instance { get; private set; }

		public GameCore()
		{
			Window.Title = "Tre Altre Volte";
			Content.RootDirectory = "Content";

			new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 800,
				PreferredBackBufferHeight = 600
			};
			Instance = this;
		}

		protected override void Initialize()
		{
			var background = new TileBackground();
			var marian = new Marian();

			TileMatrix.Initialize("Content/map.csv");
			AddContent(background);
			AddContent(marian);

			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}

			base.Update(gameTime);
		}
	}
}
