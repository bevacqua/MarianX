using System.Collections.Generic;
using System.IO;
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
		private SongManager songManager;

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
			SetLevelByIndex(Config.Start);
		}

		private void InitializeMap()
		{
			int levelCount = 2;

			for (int i = 1; i <= levelCount; i++)
			{
				LevelBackground level = new LevelBackground(i);
				levels.Add(level);
			}
		}

		private void InitializeMarian()
		{
			if (marian == null)
			{
				marian = new Marian();
				marian.Move += ViewportManager.CharacterMove;
			}
			AddAndTrack(marian);

			var collisionDetection = marian.Movement.CollisionDetection as IGameContent;
			if (collisionDetection != null)
			{
				AddContent(collisionDetection);
			}

			MobileCollisionDetection = new MobileCollisionDetectionEngine(marian);
		}

		private void InitializeEffects()
		{
			if (songManager == null)
			{
				songManager = new SongManager();
			}
			AddContent(songManager);
		}

		public int LevelIndex { get; private set; }

		public void AdvanceLevel()
		{
			SetLevelByIndex(++LevelIndex);
		}

		public void SetLevelByIndex(int index)
		{
			LevelBackground target = levels[index];
			TileMatrix.Use(target);

			LevelIndex = index;
			LoadLevel(target);
		}

		private void LoadLevel(LevelBackground background)
		{
			ClearContent();
			ViewportManager.Clear();

			AddAndTrack(background);

			InitializeMarian();
			InitializeEffects();

			AddAndTrackNpcs();

			base.Initialize();
		}

		private void AddAndTrackNpcs()
		{
			IList<NpcRecord> locations = TileMatrix.GetNpcLocations();

			foreach (NpcRecord location in locations)
			{
				Gloop gloop = new Gloop(location.Position);
				AddAndTrack(gloop);
			}
		}

		protected void AddAndTrack(IGameContent content)
		{
			AddContent(content);
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
