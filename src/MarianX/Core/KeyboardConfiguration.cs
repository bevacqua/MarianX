using System.Collections.Generic;
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
				{ActionKey.Jump, Keys.Up}
			};
		}

		private KeyboardState keyboardState;

		public KeyboardConfiguration(KeyboardState keyboardState)
		{
			this.keyboardState = keyboardState;
		}

		public bool IsKeyDown(ActionKey key)
		{
			Keys combination = config[key];
			return keyboardState.IsKeyDown(combination);
		}
	}
}