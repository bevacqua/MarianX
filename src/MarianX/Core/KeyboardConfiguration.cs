using System.Collections.Generic;
using System.Linq;
using MarianX.Enum;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public class KeyboardConfiguration
	{
		private static readonly IDictionary<ActionKey, Keys> config;

		static KeyboardConfiguration()
		{
			config = new Dictionary<ActionKey, Keys>
			{
				{ActionKey.Left, Keys.Left},
				{ActionKey.Right, Keys.Right},
				{ActionKey.Jump, Keys.Up},
				{ActionKey.ToggleMusic, Keys.F12},
				{ActionKey.ToggleDiagnosticMode, Keys.F8},
				{ActionKey.ToggleDebugMode, Keys.Divide},
				{ActionKey.UpdateInNextFrame, Keys.Multiply},
				{ActionKey.Suicide, Keys.Subtract},
			};
		}

		private KeyboardState keyboardState;

		public KeyboardConfiguration(KeyboardState keyboardState)
		{
			this.keyboardState = keyboardState;
		}

		public bool IsKeyDown(ActionKey key)
		{
			bool down = IsKeyDown(key, keyboardState);
			return down;
		}

		private bool IsKeyDown(ActionKey key, KeyboardState state)
		{
			Keys combination = config[key];
			return state.IsKeyDown(combination);
		}

		public bool IsKeyPressed(ActionKey key, KeyboardState oldState)
		{
			bool down = IsKeyDown(key);
			bool wasDown = IsKeyDown(key, oldState);

			return down && !wasDown;
		}

		public bool IsAnyKeyPressed(KeyboardState oldState)
		{
			Keys[] oldKeys = oldState.GetPressedKeys();
			Keys[] newKeys = keyboardState.GetPressedKeys();
			return !newKeys.All(oldKeys.Contains);
		}
	}
}