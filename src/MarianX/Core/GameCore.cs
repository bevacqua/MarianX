using MarianX.Diagnostics;
using MarianX.Effects;
using MarianX.Interface;
using MarianX.Mobiles;
using MarianX.Sprites;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class GameCore : DebuggableGame
	{
		public static GameCore Instance { get; private set; }

		private readonly ViewportManager viewportManager;

		public GameCore()
		{
			Instance = this;

			Window.Title = "Tre Altre Volte (e tutto questo)";
			Content.RootDirectory = "Content";

			new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 800,
				PreferredBackBufferHeight = 600
			};

			IsMouseVisible = true;
			viewportManager = new ViewportManager();
		}

		protected override void Initialize()
		{
			viewportManager.Initialize();
			var background = new MapBackground("Content/Map/map.idx", "Map/map_{0}_{1}");
			var marian = new Marian();

			marian.Move += viewportManager.CharacterMove;

			TileMatrix.Initialize("Content/Map/map.csv");
			AddManagedContent(background);
			AddManagedContent(marian);

			var collisionDetection = marian.Movement.CollisionDetection as IGameContent;
			if (collisionDetection != null)
			{
				AddContent(collisionDetection);
			}
			IGameContent songManager = new SongManager();
			AddContent(songManager);

			base.Initialize();
		}

		protected void AddManagedContent(IGameContent content)
		{
			base.AddContent(content);
			viewportManager.Manage(content);
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
