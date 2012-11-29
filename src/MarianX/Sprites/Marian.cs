using System.Collections.Generic;
using MarianX.Contents;
using MarianX.Keyboard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Sprites
{
	public class Marian : Mobile
	{
		private const string ASSET_NAME = "marian";
		private const int FRAME_WIDTH = 48;
		private const int FRAME_HEIGHT = 64;

		private const int IDLE = 0;
		private const int RIGHT_WALK = 1;
		private const int LEFT_WALK = 2;

		private static readonly SpriteSheetSettings settings;

		static Marian()
		{
			settings = new SpriteSheetSettings
			{
				Width = FRAME_WIDTH,
				Height = FRAME_HEIGHT,
				FrameSets = new List<FrameSet>
				{
					new FrameSet {Row = 0, Frames = 1},
					new FrameSet {Row = 1, Frames = 3},
					new FrameSet {Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally}
				}
			};
		}

		private KeyboardState _keyboardState;

		public Marian(Viewport viewport)
			: base(ASSET_NAME, settings)
		{
			Position = new Vector2(125, viewport.Height - FRAME_HEIGHT);
			Speed = new Vector2(160, 80);
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			UpdateMovement(keyboardState);

			_keyboardState = keyboardState;
			base.Update(gameTime);
		}

		private void UpdateMovement(KeyboardState keyboardState)
		{
			var kb = new KeyboardConfiguration(keyboardState);
			if (kb.IsShortcutDown(Action.Right))
			{
				if (Direction != Direction.Right)
				{
					SetFrameSet(RIGHT_WALK);
					Direction = Direction.Right;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (Direction != Direction.Left)
				{
					SetFrameSet(LEFT_WALK);
					Direction = Direction.Left;
				}
			}
			else if (Direction != Direction.None)
			{
				SetFrameSet(IDLE);
				Direction = Direction.None;
			}
		}
	}
}