using System.Collections.Generic;
using MarianX.Contents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Sprites
{
	public class Marian : Mobile
	{
		private static readonly SpriteSheetSettings settings;

		static Marian()
		{
			settings = new SpriteSheetSettings
			{
				Width = 48,
				Height = 64,
				FrameSets = new List<FrameSet>
				{
					new FrameSet {Count = 2}
				}
			};
		}

		private const string ASSET_NAME = "marian";
		private KeyboardState _keyboardState;

		public Marian()
			: base(ASSET_NAME, settings)
		{
			Position = new Vector2(125, 245);
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
			if (kb.IsShortcutDown(Action.Left))
			{
				Direction = Direction.Left;
			}
			else if (kb.IsShortcutDown(Action.Right))
			{
				Direction = Direction.Right;
			}
			else if (kb.IsShortcutDown(Action.Jump))
			{
				Direction = Direction.Up;
			}
			else if (kb.IsShortcutDown(Action.Crutch))
			{
				Direction = Direction.Down;
			}
			else
			{
				Direction = Direction.None;
			}
		}
	}
}