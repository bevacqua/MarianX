using MarianX.Diagnostics;
using MarianX.Effects;
using MarianX.Interface;
using MarianX.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class GameCore : DebuggableGame
	{
		public static GameCore Instance { get; private set; }

		public ViewportManager ViewportManager { get; private set; }

		public DynamicCollisionDetectionEngine DynamicCollisionDetection { get; set; }

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
		}

		protected override void Initialize()
		{
			ViewportManager.Initialize();
			LevelManager.Instance.Initialize();
		}

		public void InitializeBase()
		{
			base.Initialize();
		}

		public void AdvanceLevel()
		{
			LevelManager.Instance.AdvanceLevel();
		}

		public void AddAndTrack(IGameContent content)
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
