using System.Collections.Generic;
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
		private readonly IList<LevelBackground> levels;

		private Marian marian;

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
			levels = new List<LevelBackground>();
		}

		protected override void Initialize()
		{
			viewportManager.Initialize();

			InitializeMap();
			InitializeMarian();
			InitializeEffects();
			SetLevel(0);

			base.Initialize();
		}

		private void InitializeMap()
		{
			levels.Add(new LevelBackground(1));

			foreach (LevelBackground level in levels)
			{
				AddManagedContent(level);
			}
		}

		private void InitializeMarian()
		{
			marian = new Marian();
			marian.Move += viewportManager.CharacterMove;

			AddManagedContent(marian);


			var collisionDetection = marian.Movement.CollisionDetection as IGameContent;
			if (collisionDetection != null)
			{
				AddContent(collisionDetection);
			}
		}

		private void InitializeEffects()
		{
			AddContent(new SongManager());
		}

		public void SetLevel(int index)
		{
			LevelBackground level = levels[index];
			TileMatrix.Use(level);

			marian.Initialize();
		}

		protected void AddManagedContent(IGameContent content)
		{
			base.AddContent(content);
			viewportManager.Manage(content);
		}

		protected override void UpdateInput(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}

			base.UpdateInput(gameTime);
		}
	}
}
