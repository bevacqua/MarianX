using System;
using MarianX.Effects;
using MarianX.Geometry;
using MarianX.Interface;
using MarianX.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class GameCore : PausableGame
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
			Restart();
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

		protected override void Restart()
		{
			LevelManager.Instance.Initialize();
		}

		private TimeSpan? blackOutStart;
		private TimeSpan? blackOutDuration;

		public void BlackOut(GameTime gameTime, TimeSpan duration)
		{
			blackOutStart = gameTime.TotalGameTime;
			blackOutDuration = duration;
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (!blackOutStart.HasValue || !blackOutDuration.HasValue)
			{
				return;
			}

			var difference = gameTime.TotalGameTime - blackOutStart.Value;
			if (difference > blackOutDuration)
			{
				blackOutStart = null;
				blackOutDuration = null;
			}
			else
			{
				double alpha = 1 - difference.TotalMilliseconds / blackOutDuration.Value.TotalMilliseconds;

				Square square = new Square
				{
					Bounds = GraphicsDevice.Viewport.Bounds,
					Color = Color.Black,
					Alpha = (float)Math.Max(0.01, Math.Min(alpha, 1))
				};

				spriteBatch.Begin();
				square.Draw(spriteBatch);
				spriteBatch.End();
			}
		}
	}
}
