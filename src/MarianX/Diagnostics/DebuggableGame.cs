using MarianX.Core;
using MarianX.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Diagnostics
{
	public class DebuggableGame : DiagnosticGame
	{
		private bool debugMode;
		private bool updateInNextFrame;
		private KeyboardState oldState;

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			updateInNextFrame = false; // only really matters for debug mode.
		}

		protected override void UpdateInput(GameTime gameTime)
		{
			UpdateDebugInput();

			if (debugMode && !updateInNextFrame)
			{
				return;
			}
			base.UpdateInput(gameTime);
		}

		private void UpdateDebugInput()
		{
			KeyboardState keyboardState = Keyboard.GetState();
			KeyboardConfiguration kb = new KeyboardConfiguration(keyboardState);

			if (kb.IsKeyPressed(ActionKey.ToggleDiagnosticMode, oldState))
			{
				Config.Diagnostic = !Config.Diagnostic;
			}

			if (Config.Diagnostic) // debug mode only available in diagnostic configuration.
			{
				if (kb.IsKeyPressed(ActionKey.ToggleDebugMode, oldState))
				{
					debugMode = !debugMode;
				}

				if (kb.IsKeyPressed(ActionKey.UpdateInNextFrame, oldState))
				{
					debugMode = true;
					updateInNextFrame = true;
				}
			}
			oldState = keyboardState;
		}

		protected override void UpdateOutput(GameTime gameTime)
		{
			if (debugMode && !updateInNextFrame)
			{
				return;
			}
			base.UpdateOutput(gameTime);
		}
	}
}