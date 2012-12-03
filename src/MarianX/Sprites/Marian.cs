using System.Collections.Generic;
using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Input;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Sprites
{
	public class Marian : DiagnosticMobile
	{
		private const string AssetName = "marian";
		private const int FrameWidth = MagicNumbers.MarianFrameWidth;
		private const int FrameHeight = MagicNumbers.MarianFrameHeight;

		private const int Idle = 0;
		private const int WalkRight = 1;
		private const int WalkLeft = 2;

		private static readonly SpriteSheetSettings settings;

		static Marian()
		{
			settings = new SpriteSheetSettings
			{
				Width = FrameWidth,
				Height = FrameHeight,
				FrameSets = new List<FrameSet>
				{
					new FrameSet {Row = 0, Frames = 1},
					new FrameSet {Row = 1, Frames = 3},
					new FrameSet {Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally}
				}
			};
		}

		private readonly Viewport viewport;

		private KeyboardState _keyboardState;

		public Marian(Viewport viewport)
			: base(AssetName, settings)
		{
			this.viewport = viewport;

			BoundingBox = new MarianBoundingBox();
		}

		public override void Initialize()
		{
			base.Initialize();

			Position = new Vector2(125, viewport.Height - FrameHeight);
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
			Direction previous = Direction;

			var kb = new KeyboardConfiguration(keyboardState);
			if (kb.IsShortcutDown(Action.Right))
			{
				if (Direction != Direction.Right)
				{
					SetFrameSet(WalkRight);
					Direction = Direction.Right;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (Direction != Direction.Left)
				{
					SetFrameSet(WalkLeft);
					Direction = Direction.Left;
				}
			}
			else if (Direction != Direction.None)
			{
				SetFrameSet(Idle);
				Direction = Direction.None;
			}

			if (Direction != previous)
			{
				Speed = Vector2.Zero;
			}
		}
	}
}