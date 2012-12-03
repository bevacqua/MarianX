using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Input
{
	public class KeyboardConfiguration
	{
		private static IDictionary<Action, Keys> config;

		static KeyboardConfiguration()
		{
			config = new Dictionary<Action, Keys>
			{
				{Action.Left, Keys.Left},
				{Action.Right, Keys.Right},
				{Action.Jump, Keys.Up},
				{Action.Crutch, Keys.Down}
			};
		}

		private KeyboardState keyboardState;

		public KeyboardConfiguration(KeyboardState keyboardState)
		{
			this.keyboardState = keyboardState;
		}

		public bool IsShortcutDown(Action action)
		{
			Keys combination = config[action];
			return keyboardState.IsKeyDown(combination);
		}

		public bool IsUntouched()
		{
			Keys[] keys = keyboardState.GetPressedKeys();
			bool touched = config.Values.Any(keys.Contains);
			return !touched;
		}
	}
}