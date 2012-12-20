using System.Collections.Generic;
using MarianX.Diagnostics;
using MarianX.Effects;
using MarianX.Interface;
using MarianX.Mobiles;
using MarianX.Physics;
using MarianX.Sprites;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class GameCore : DebuggableGame
	{
		public static GameCore Instance { get; private set; }

		private readonly IList<LevelBackground> levels;

		public ViewportManager ViewportManager { get; private set; }

		private Marian marian;
		public MobileCollisionDetectionEngine MobileCollisionDetection { get; private set; }

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
			ViewportManager = new ViewportManager();
			levels = new List<LevelBackground>();
			LevelIndex = -1;
		}

		protected override void Initialize()
		{
			ViewportManager.Initialize();

			InitializeMap();
			InitializeMarian();
			InitializeEffects();
			AdvanceLevel();

			Gloop gloop = new Gloop(new Vector2(90, 60));
			AddManagedContent(gloop);

			base.Initialize();
		}

		private void InitializeMap()
		{
			int levelCount = 2;

			for (int i = 1; i <= levelCount; i++)
			{
				levels.Add(new LevelBackground(i));
			}

			foreach (LevelBackground level in levels)
			{
				AddManagedContent(level);
			}
		}

		private void InitializeMarian()
		{
			marian = new Marian();
			marian.Move += ViewportManager.CharacterMove;

			AddManagedContent(marian);

			var collisionDetection = marian.Movement.CollisionDetection as IGameContent;
			if (collisionDetection != null)
			{
				AddContent(collisionDetection);
			}

			MobileCollisionDetection = new MobileCollisionDetectionEngine(marian);
		}

		private void InitializeEffects()
		{
			AddContent(new SongManager());
		}

		public int LevelIndex { get; private set; }

		public void AdvanceLevel()
		{
			SetLevelByIndex(++LevelIndex);
		}

		public void SetLevelByIndex(int index)
		{
			foreach (LevelBackground level in levels)
			{
				level.Active = false;
			}
			LevelBackground target = levels[index];
			TileMatrix.Use(target);
			target.Active = true;

			LevelIndex = index;
			marian.Initialize();
		}

		protected void AddManagedContent(IGameContent content)
		{
			base.AddContent(content);
			ViewportManager.Manage(content);
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
